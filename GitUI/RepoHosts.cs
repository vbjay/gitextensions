using System.Collections.Generic;
using System.Linq;
using GitCommands;
using GitUIPluginInterfaces.RepositoryHosts;

namespace GitUI
{
    public class RepoHosts
    {
        static RepoHosts()
        {
            GitHosters = new List<IRepositoryHostPlugin>();
        }

        public static List<IRepositoryHostPlugin> GitHosters { get; private set; }

        public static IRepositoryHostPlugin TryGetGitHosterForModule(GitModule aModule)
        {
            if (!aModule.IsValidGitWorkingDir())
                return null;

            return GitHosters.FirstOrDefault(gitHoster => gitHoster.GitModuleIsRelevantToMe(aModule));
        }
    }
}
