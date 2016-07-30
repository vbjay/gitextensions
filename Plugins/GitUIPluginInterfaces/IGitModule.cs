using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GitUIPluginInterfaces
{
    /// <summary>Provides manipulation with git module.</summary>
    public interface IGitModule
    {
        Version AppVersion { get; }

        /// <summary>Gets the path to the git application executable.</summary>
        string GitCommand { get; }

        string GravatarCacheDir { get; }

        /// <summary>Gets the directory which contains the git repository.</summary>
        string WorkingDir { get; }

        ISettingsSource GetEffectiveSettings();

        /// <summary>Gets the ".git" directory path.</summary>
        string GetGitDirectory();

        string[] GetRemotes(bool allowEmpty);

        /// <summary>Gets the current branch; or "(no branch)" if HEAD is detached.</summary>
        string GetSelectedBranch();

        string GetSetting(string setting);

        IGitModule GetSubmodule(string submoduleName);

        IEnumerable<IGitSubmoduleInfo> GetSubmodulesInfo();

        IList<string> GetSubmodulesLocalPaths(bool recursive = true);

        /// <summary>true if ".git" directory does NOT exist.</summary>
        bool IsBareRepository();

        bool IsRunningGitProcess();

        /// <summary>Indicates whether the specified directory contains a git repository.</summary>
        bool IsValidGitWorkingDir();

        string RunBatchFile(string batchFile);

        /// <summary>
        /// Run command, console window is hidden, wait for exit, redirect output
        /// </summary>
        string RunCmd(string cmd, string arguments, Encoding encoding = null, byte[] stdIn = null);

        /// <summary>
        /// Run command, console window is hidden, wait for exit, redirect output
        /// </summary>
        CmdResult RunCmdResult(string cmd, string arguments, Encoding encoding = null, byte[] stdInput = null);

        /// <summary>
        /// Run git command, console window is hidden, wait for exit, redirect output
        /// </summary>
        string RunGitCmd(string arguments, Encoding encoding = null, byte[] stdInput = null);

        /// <summary>
        /// Run git command, console window is hidden, redirect output
        /// </summary>
        Process RunGitCmdDetached(string arguments, Encoding encoding = null);

        /// <summary>
        /// Run git command, console window is hidden, wait for exit, redirect output
        /// </summary>
        CmdResult RunGitCmdResult(string arguments, Encoding encoding = null, byte[] stdInput = null);

        bool StartPageantForRemote(string remote);
    }
}
