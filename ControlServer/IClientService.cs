using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BuildWatch.ControlServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IClientService" in both code and config file together.
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract]
        int GetProtocolVersion();

        [OperationContract]
        PollBuildStatusResp PollBuildStatus(PollBuildStatusReq req);

    }

#region PollBuildStatus

    [DataContract]
    public class PollBuildStatusReq
    {
        [DataMember]
        public int ConfigurationId { get; set; }

        [DataMember]
        public long UpdateCounter { get; set; }

    }

    [DataContract]
    public class PollBuildStatusResp
    {
        [DataMember]
        public long UpdateCounter { get; set; }

        [DataMember]
        public List<FinishedBuild> FinishedBuilds { get; set; }

        [DataMember]
        public DateTime FinishedBuildsDate { get; set; }

        [DataMember]
        public List<QueuedBuild> QueuedBuilds { get; set; }

        [DataMember]
        public List<ClientEvent> ClientEvents { get; set; }
    }

    [DataContract]
    public class FinishedBuild
    {
        [DataMember]
        public string BuildName { get; set; }

        [DataMember]
        public string BuildInstance { get; set; }

        [DataMember]
        public DateTime TimeStamp { get; set; }

        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string State { get; set; }
    }

    [DataContract]
    public class QueuedBuild
    {
        [DataMember]
        public string BuildName { get; set; }

        [DataMember]
        public DateTime QueueTime { get; set; }
    }

    [DataContract]
    public class ClientEvent
    {

    }

#endregion

}
