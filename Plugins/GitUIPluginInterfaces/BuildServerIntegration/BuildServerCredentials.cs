namespace GitUIPluginInterfaces.BuildServerIntegration
{
    public class BuildServerCredentials : IBuildServerCredentials
    {
        public string Password { get; set; }
        public bool UseGuestAccess { get; set; }

        public string Username { get; set; }
    }
}
