namespace GitUIPluginInterfaces.BuildServerIntegration
{
    public interface IBuildServerCredentials
    {
        string Password { get; set; }
        bool UseGuestAccess { get; set; }

        string Username { get; set; }
    }
}
