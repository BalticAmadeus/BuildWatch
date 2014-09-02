using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BuildWatch.ControlServer
{
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        int GetProtocolVersion();

        [OperationContract]
        GetDataSourceConfigResponse GetDataSourceConfig(GetDataSourceConfigRequest req);

        [OperationContract]
        PushFinishedBuildsResponse PushFinishedBuilds(PushFinishedBuildsRequest req);
    }

#region GetDataSourceConfig

    [DataContract]
    public class GetDataSourceConfigRequest
    {
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class GetDataSourceConfigResponse
    {
        [DataMember]
        public int DataSourceId { get; set; }

        [DataMember]
        public List<ConfigEntry> ConfigEntries { get; set; } 
    }

    [DataContract]
    public class ConfigEntry
    {
        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public string Value { get; set; }
    }

#endregion

#region PushFinishedBuilds

    [DataContract]
    public class PushFinishedBuildsRequest
    {
        [DataMember]
        public int DataSourceId { get; set; }

        [DataMember]
        public List<FinishedBuildInfo> BuildInfo { get; set; }

        [DataMember]
        public List<QueuedBuildInfo> QueuedBuilds { get; set; }

    }

    [DataContract]
    public class PushFinishedBuildsResponse
    {
        [DataMember]
        public List<QueryInfo> AdditionalQueries { get; set; }
    }

    [DataContract]
    public class FinishedBuildInfo
    {
        [DataMember]
        public string BuildInstance { get; set; }

        [DataMember]
        public string BuildName { get; set; }

        [DataMember]
        public DateTime TimeStamp { get; set; }

        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string UserAction { get; set; }
    }

    [DataContract]
    public class QueuedBuildInfo
    {
        [DataMember]
        public string BuildName { get; set; }

        [DataMember]
        public DateTime QueueTime { get; set; }
    }

    [DataContract]
    public class QueryInfo
    {
        [DataMember]
        public QueryBuildRangeInfo QueryBuildRange { get; set; }
    }

    [DataContract]
    public class QueryBuildRangeInfo
    {
        [DataMember]
        public string BuildName { get; set; }

        [DataMember]
        public DateTime RangeFrom { get; set; }

        [DataMember]
        public DateTime RangeTo { get; set; }
    }

#endregion

}
