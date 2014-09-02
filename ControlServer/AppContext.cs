using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildWatch.ControlServer
{
    public class AppContext
    {
        #region Static stuff

        private static object _staticSync = new object();
        private static AppContext _globalInstance = null;

        public static AppContext Current
        {
            get
            {
                if (_globalInstance != null)
                    return _globalInstance;
                lock (_staticSync)
                {
                    if (_globalInstance == null)
                        _globalInstance = new AppContext();
                    return _globalInstance;
                }
            }
        }

        #endregion

        public PollBuildStatusResp CachedPollBuildStatusResp { get; private set; }

        private AppContext()
        {
            CachedPollBuildStatusResp = new PollBuildStatusResp {
                UpdateCounter = 0,
                FinishedBuilds = new List<FinishedBuild>()
            };
        }

       public void SetResponse(PollBuildStatusResp resp)
        {
            lock (this)
            {
                CachedPollBuildStatusResp = resp;
            }
        }
    }
}