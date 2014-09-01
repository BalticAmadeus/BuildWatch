﻿using System;
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

        public List<FinishedBuild> FinishedBuilds { get; private set; }
        public DateTime FinishedBuildsDate { get; private set; }

        private AppContext()
        {
            FinishedBuilds = new List<FinishedBuild>();
        }

       public void SetBuilds(List<FinishedBuild> finishedBuilds)
        {
            lock (this)
            {
                FinishedBuilds = finishedBuilds;
                FinishedBuildsDate = DateTime.Now;
            }
        }
    }
}