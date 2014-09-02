using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BuildWatch.ControlServer
{
    public class ClientService : IClientService
    {
        public int GetProtocolVersion()
        {
            return 0;
        }

        public PollBuildStatusResp PollBuildStatus(PollBuildStatusReq req)
        {
            if (req.ConfigurationId != 1)
                throw new ApplicationException(string.Format("ConfigurationId={0} is not defined", req.ConfigurationId));

            var resp = new PollBuildStatusResp
            {
                UpdateCounter = 0
            };

            var app = AppContext.Current;
            lock (app)
            {
                resp.FinishedBuilds = app.FinishedBuilds;
                resp.FinishedBuildsDate = app.FinishedBuildsDate;
            }

            return resp;
        }
    }
}
