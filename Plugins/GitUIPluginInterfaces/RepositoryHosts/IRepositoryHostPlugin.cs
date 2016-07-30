using System.Collections.Generic;

namespace GitUIPluginInterfaces.RepositoryHosts
{
    public interface IRepositoryHostPlugin : IGitPlugin
    {
        bool ConfigurationOk { get; }

        List<IHostedRemote> GetHostedRemotesForModule(IGitModule aModule);

        IList<IHostedRepository> GetMyRepos();

        IList<IHostedRepository> GetRepositoriesOfUser(string user);

        IHostedRepository GetRepository(string user, string repositoryName);

        bool GitModuleIsRelevantToMe(IGitModule aModule);

        IList<IHostedRepository> SearchForRepository(string search);
    }
}
