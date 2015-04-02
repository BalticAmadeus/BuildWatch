using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BuildWatch.ControlServer
{
    [ServiceContract]
    public interface IFileService
    {
        [OperationContract]
        GetPictureResp GetPicture(GetPictureReq req);
    }

    #region GetPicture

    [MessageContract]
    public class GetPictureReq
    {
        [MessageBodyMember]
        public string UserName;
    }

    [MessageContract]
    public class GetPictureResp
    {
        [MessageHeader]
        public string PictureHash;

        [MessageBodyMember]
        public Stream PictureData;
    }

    #endregion
}
