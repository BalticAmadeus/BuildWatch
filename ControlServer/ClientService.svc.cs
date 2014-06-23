using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BuildWatch.ControlServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ClientService" in code, svc and config file together.
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

            resp.FinishedBuilds = AppContext.Current.FinishedBuilds;

            return resp;
        }
    }
}
