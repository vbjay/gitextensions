﻿namespace GitCommands.Config
{
    /// <summary>
    /// Defines the strings to access certain git config settings.
    /// Goal is to eliminate duplicate string constants in the code.
    /// </summary>
    public static class SettingKeyString
    {
        /// <summary>
        /// "branch.{0}.remote"
        /// </summary>
        public static string BranchRemote = "branch.{0}.remote";

        /// <summary>
        /// commit.gpgsign  Determines if commits are signed by default
        /// </summary>
        public static string CommitGPGSign = "commit.gpgsign";

        /// <summary>
        /// "credential.helper"
        /// </summary>
        public static string CredentialHelper = "credential.helper";

        /// <summary>
        /// diff.guitool
        /// </summary>
        public static string DiffToolKey = "diff.guitool";

        /// <summary>
        /// gpg.program  Stores the path to the gpg program
        /// </summary>
        public static string GPGProgram = "gpg.program";

        /// <summary>
        /// merge.guitool, requires Git 2.20.0
        /// </summary>
        public static string MergeToolKey = "merge.guitool";

        /// <summary>
        /// merge.tool
        /// </summary>
        public static string MergeToolNoGuiKey = "merge.tool";

        /// <summary>
        /// "remote.{0}.push"
        /// </summary>
        public static string RemotePush = "remote.{0}.push";

        /// <summary>
        /// "remote.{0}.pushurl"
        /// </summary>
        public static string RemotePushUrl = "remote.{0}.pushurl";

        /// <summary>
        /// "remote.{0}.puttykeyfile"
        /// </summary>
        public static string RemotePuttySshKey = "remote.{0}.puttykeyfile";

        /// <summary>
        /// "remote.{0}.url"
        /// </summary>
        public static string RemoteUrl = "remote.{0}.url";

        /// <summary>
        /// user.email
        /// </summary>
        public static string UserEmail = "user.email";

        /// <summary>
        /// user.name
        /// </summary>
        public static string UserName = "user.name";

        /// <summary>
        /// user.signingkey  Stores the key ID to use when signing a commit with GPG
        /// </summary>
        public static string UserSigningKey = "user.signingkey";
    }
}
