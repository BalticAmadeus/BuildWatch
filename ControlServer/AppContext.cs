using BuildWatch.ControlServer.Properties;
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

        private PictureCache _pictureCache;

        public PollBuildStatusResp CachedPollBuildStatusResp { get; private set; }

        private AppContext()
        {
            _pictureCache = new PictureCache(Settings.Default.PictureDir);
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

        public PollBuildStatusResp PollBuildStatus()
        {
            lock (this) {
                PollBuildStatusResp resp = CachedPollBuildStatusResp;
                // NOTE that we are actually updating the cached version so PictureMaps != null on repeated get
                if (resp != null && resp.FinishedBuilds != null && resp.PictureMaps == null)
                {
                    HashSet<string> users = new HashSet<string>();
                    users.UnionWith(resp.FinishedBuilds.Select(b => b.UserName));
                    List<PictureMap> picMaps = new List<PictureMap>();
                    foreach (string userName in users)
                    {
                        PictureMap pm = new PictureMap();
                        pm.UserName = userName;
                        pm.PictureHash = _pictureCache.GetForUser(userName).Hash;
                        picMaps.Add(pm);
                    }
                    resp.PictureMaps = picMaps;
                }
                return resp;
            }
        }

        public PictureCacheEntry GetUserPicture(string userName)
        {
            lock (this)
            {
                return _pictureCache.GetForUser(userName);
            }
        }
    }
}