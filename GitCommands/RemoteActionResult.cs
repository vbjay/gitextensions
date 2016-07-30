namespace GitCommands
{
    public class RemoteActionResult<R>
    {
        public bool AuthenticationFail { get; set; }
        public bool HostKeyFail { get; set; }
        public R Result { get; set; }
    }
}
