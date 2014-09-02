using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BuildWatch.DataSource.Common
{
    public abstract class DataSourceBase : IDataSource
    {
        private Regex buildNameMatch;
        private string buildNameReplace;

        public virtual void Initialize(DataSourceConfig config)
        {
            string bnm = config.Get("BuildNameMatch", null);
            if (!string.IsNullOrEmpty(bnm))
                buildNameMatch = new Regex(bnm);
            buildNameReplace = config.Get("BuildNameReplace", null);
        }

        public abstract void Poll(DataService.IDataService dataService, System.Threading.CancellationToken quitToken);

        protected bool TryMatchBuildName(ref string buildName)
        {
            if (buildNameMatch == null)
                return true;
            Match m = buildNameMatch.Match(buildName);
            if (!m.Success)
                return false;
            if (buildNameReplace == null)
                return true;
            buildName = string.Format(buildNameReplace, m.Groups.Cast<object>().ToArray());
            return true;
        }
    }
}
