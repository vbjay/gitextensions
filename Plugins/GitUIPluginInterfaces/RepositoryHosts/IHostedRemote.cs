namespace GitUIPluginInterfaces.RepositoryHosts
{
    public interface IHostedRemote
    {
        string Data { get; }

        string DisplayData { get; }

        bool IsOwnedByMe { get; }

        string Name { get; }

        IHostedRepository GetHostedRepository();

        //This is the name of the remote in the local git repository. This might be null
    }
}
