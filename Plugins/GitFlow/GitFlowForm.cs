﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GitCommands;
using GitFlow.Properties;
using GitUIPluginInterfaces;
using ResourceManager;

namespace GitFlow
{
    public partial class GitFlowForm : GitExtensionsFormBase
    {
        private const string RefHeads = "refs/heads/";
        private readonly TranslationString _gitFlowTooltip = new TranslationString("A good branch model for your project with Git...");
        private readonly GitUIBaseEventArgs _gitUiCommands;
        private readonly TranslationString _loading = new TranslationString("Loading...");
        private readonly TranslationString _noBranchExist = new TranslationString("No {0} branches exist.");
        private readonly AsyncLoader _task = new AsyncLoader();

        public GitFlowForm(GitUIBaseEventArgs gitUiCommands)
        {
            InitializeComponent();
            Translate();

            _gitUiCommands = gitUiCommands;

            Branches = new Dictionary<string, List<string>>();

            lblPrefixManage.Text = string.Empty;
            ttGitFlow.SetToolTip(lnkGitFlow, _gitFlowTooltip.Text);

            if (_gitUiCommands != null)
                Init();
        }

        private enum Branch
        {
            feature,
            hotfix,
            release,
            support
        }

        public bool IsRefreshNeeded { get; set; }
        private Dictionary<string, List<string>> Branches { get; set; }

        private List<string> BranchTypes
        {
            get { return Enum.GetValues(typeof(Branch)).Cast<object>().Select(e => e.ToString()).ToList(); }
        }

        private string CurrentBranch { get; set; }

        private bool IsGitFlowInited
        {
            get { return !string.IsNullOrWhiteSpace(_gitUiCommands.GitModule.RunGitCmd("config --get gitflow.branch.master")); }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbBasedOn_CheckedChanged(object sender, EventArgs e)
        {
            cbBaseBranch.Enabled = cbBasedOn.Checked;
        }

        private void DisplayHead()
        {
            var head = _gitUiCommands.GitModule.RunGitCmd("symbolic-ref HEAD").Trim('*', ' ', '\n', '\r');
            lblHead.Text = head;
            var currentRef = head.StartsWith(RefHeads) ? head.Substring(RefHeads.Length) : head;

            string branchTypes;
            string branchName;
            if (TryExtractBranchFromHead(currentRef, out branchTypes, out branchName))
            {
                cbManageType.SelectedItem = branchTypes;
                CurrentBranch = branchName;
            }
        }

        private void Init()
        {
            var isGitFlowInited = IsGitFlowInited;

            btnInit.Visible = !isGitFlowInited;
            gbStart.Enabled = isGitFlowInited;
            gbManage.Enabled = isGitFlowInited;
            lblCaptionHead.Visible = isGitFlowInited;
            lblHead.Visible = isGitFlowInited;

            if (isGitFlowInited)
            {
                var remotes = _gitUiCommands.GitModule.GetRemotes(true).Where(r => !string.IsNullOrWhiteSpace(r)).ToList();
                cbRemote.DataSource = remotes;
                btnPull.Enabled = btnPublish.Enabled = remotes.Any();

                cbType.DataSource = BranchTypes;
                var types = new List<string> { string.Empty };
                types.AddRange(BranchTypes);
                cbManageType.DataSource = types;

                cbBasedOn.Checked = false;
                cbBaseBranch.Enabled = false;
                LoadBaseBranches();

                DisplayHead();
            }
        }

        private void lnkGitFlow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/nvie/gitflow");
        }

        private bool TryExtractBranchFromHead(string currentRef, out string branchType, out string branchName)
        {
            foreach (Branch branch in Enum.GetValues(typeof(Branch)))
            {
                var startRef = branch.ToString("G") + "/";
                if (currentRef.StartsWith(startRef))
                {
                    branchType = branch.ToString("G");
                    branchName = currentRef.Substring(startRef.Length);
                    return true;
                }
            }
            branchType = null;
            branchName = null;
            return false;
        }

        #region Loading Branches

        private void DisplayBranchDatas()
        {
            var branchType = cbManageType.SelectedValue.ToString();
            var branches = Branches[branchType];
            var isThereABranch = branches.Any();

            cbManageType.Enabled = true;
            cbBranches.DataSource = isThereABranch ? branches : new List<string> { string.Format(_noBranchExist.Text, branchType) };
            cbBranches.Enabled = isThereABranch;
            if (isThereABranch && CurrentBranch != null)
            {
                cbBranches.SelectedItem = CurrentBranch;
                CurrentBranch = null;
            }

            btnFinish.Enabled = isThereABranch && (branchType != Branch.support.ToString("G"));
            btnPublish.Enabled = isThereABranch;
            btnPull.Enabled = isThereABranch;
            pnlPull.Enabled = (branchType == Branch.feature.ToString("G"));
        }

        private List<string> GetBranches(string typeBranch)
        {
            var result = _gitUiCommands.GitModule.RunGitCmdResult("flow " + typeBranch);
            if (result.ExitCode != 0)
                return new List<string>();
            string[] references = result.StdOutput.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return references.Select(e => e.Trim('*', ' ', '\n', '\r')).ToList();
        }

        private List<string> GetLocalBranches()
        {
            string[] references = _gitUiCommands.GitModule.RunGitCmd("branch")
                                                 .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            return references.Select(e => e.Trim('*', ' ', '\n', '\r')).ToList();
        }

        private void LoadBaseBranches()
        {
            var branchType = cbType.SelectedValue.ToString();
            var manageBaseBranch = (branchType == Branch.feature.ToString("G") || branchType == Branch.hotfix.ToString("G") || branchType == Branch.support.ToString("G"));
            pnlBasedOn.Visible = manageBaseBranch;

            if (manageBaseBranch)
                cbBaseBranch.DataSource = GetLocalBranches();
        }

        private void LoadBranches(string branchType)
        {
            cbManageType.Enabled = false;
            cbBranches.DataSource = new List<string> { _loading.Text };
            if (!Branches.ContainsKey(branchType))
                _task.Load(() => GetBranches(branchType), (branches) => { Branches.Add(branchType, branches); DisplayBranchDatas(); });
            else
                DisplayBranchDatas();
        }

        #endregion Loading Branches

        #region Run GitFlow commands

        private void btnFinish_Click(object sender, EventArgs e)
        {
            RunCommand("flow " + cbManageType.SelectedValue + " finish " + cbBranches.SelectedValue);
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            if (RunCommand("flow init -d"))
                Init();
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            RunCommand("flow feature publish " + cbBranches.SelectedValue);
        }

        private void btnPull_Click(object sender, EventArgs e)
        {
            RunCommand("flow feature pull " + cbRemote.SelectedValue + " " + cbBranches.SelectedValue);
        }

        private void btnStartBranch_Click(object sender, EventArgs e)
        {
            var branchType = cbType.SelectedValue.ToString();
            if (RunCommand("flow " + branchType + " start " + txtBranchName.Text + GetBaseBranch()))
            {
                txtBranchName.Text = string.Empty;
                if (cbManageType.SelectedValue.ToString() == branchType)
                {
                    Branches.Remove(branchType);
                    LoadBranches(branchType);
                }
                else
                    Branches.Remove(branchType);
            }
        }

        private string GetBaseBranch()
        {
            var branchType = cbType.SelectedValue.ToString();
            if (branchType == Branch.release.ToString("G"))
                return string.Empty;
            if (branchType == Branch.support.ToString("G"))
                return " HEAD"; //Hoping that's a revision on master (How to get the sha of the selected line in GitExtension?)
            if (!cbBasedOn.Checked)
                return string.Empty;
            return " " + cbBaseBranch.SelectedValue;
        }

        private bool RunCommand(string commandText)
        {
            pbResultCommand.Image = Resource.StatusHourglass;
            ShowToolTip(pbResultCommand, "running command : git " + commandText);
            ForceRefresh(pbResultCommand);
            lblRunCommand.Text = "git " + commandText;
            ForceRefresh(lblRunCommand);
            txtResult.Text = "running...";
            ForceRefresh(txtResult);

            var result = _gitUiCommands.GitModule.RunGitCmdResult(commandText);

            IsRefreshNeeded = true;

            ttDebug.RemoveAll();
            ttDebug.SetToolTip(lblDebug, "cmd: git " + commandText + "\n" + "exit code:" + result.ExitCode);

            var resultText = Regex.Replace(result.GetString(), @"\r\n?|\n", Environment.NewLine);
            if (result.ExitCode == 0)
            {
                pbResultCommand.Image = Resource.success;
                ShowToolTip(pbResultCommand, resultText);
                DisplayHead();
                txtResult.Text = resultText;
            }
            else
            {
                pbResultCommand.Image = Resource.error;
                ShowToolTip(pbResultCommand, "error: " + resultText);
                txtResult.Text = resultText;
            }
            return result.ExitCode == 0;
        }

        #endregion Run GitFlow commands

        #region GUI interactions

        private void cbManageType_SelectedValueChanged(object sender, EventArgs e)
        {
            var branchType = cbManageType.SelectedValue.ToString();
            lblPrefixManage.Text = branchType + "/";
            if (!string.IsNullOrWhiteSpace(branchType))
            {
                pnlManageBranch.Enabled = true;
                LoadBranches(branchType);
            }
            else
            {
                pnlManageBranch.Enabled = false;
            }
        }

        private void cbType_SelectedValueChanged(object sender, EventArgs e)
        {
            lblPrefixName.Text = cbType.SelectedValue + "/";
            LoadBaseBranches();
        }

        private void ForceRefresh(Control c)
        {
            c.Invalidate();
            c.Update();
            c.Refresh();
        }

        private void ShowToolTip(Control c, string msg)
        {
            ttCommandResult.RemoveAll();
            ttCommandResult.SetToolTip(c, msg);
        }

        #endregion GUI interactions
    }
}
