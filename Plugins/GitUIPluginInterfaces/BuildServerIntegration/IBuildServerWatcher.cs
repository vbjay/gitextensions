namespace GitUIPluginInterfaces.BuildServerIntegration
{
    public interface IBuildServerWatcher
    {
        void CancelBuildStatusFetchOperation();

        IBuildServerCredentials GetBuildServerCredentials(IBuildServerAdapter buildServerAdapter, bool useStoredCredentialsIfExisting);

        void LaunchBuildServerInfoFetchOperation();
    }
}
