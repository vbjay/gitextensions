using System.Text;

namespace GitUIPluginInterfaces
{
    public struct CmdResult
    {
        public int ExitCode;
        public string StdError;
        public string StdOutput;

        public string GetString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.StdOutput != null && this.StdOutput.Length > 0)
                sb.Append(this.StdOutput);
            if (this.StdError != null && this.StdError.Length > 0 && this.StdOutput != null && this.StdOutput.Length > 0)
                sb.AppendLine();
            if (this.StdError != null && this.StdError.Length > 0)
                sb.Append(this.StdError);
            return sb.ToString();
        }
    }
}
