using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BuildWatch.ControlServer
{
    public class FileService : IFileService
    {
        public GetPictureResp GetPicture(GetPictureReq req)
        {
            var resp = new GetPictureResp();
            PictureCacheEntry entry = AppContext.Current.GetUserPicture(req.UserName);
            resp.PictureHash = entry.Hash;
            resp.PictureData = new MemoryStream(entry.Data);
            return resp;
        }
    }
}
