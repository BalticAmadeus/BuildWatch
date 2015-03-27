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

            return AppContext.Current.PollBuildStatus();
        }


        public GetPicturesResp GetPictures(GetPicturesReq req)
        {
            var resp = new GetPicturesResp();
            List<PictureInfo> pics = new List<PictureInfo>();
            foreach (string userName in req.UserNames)
            {
                PictureCacheEntry entry = AppContext.Current.GetUserPicture(userName);
                pics.Add(new PictureInfo
                {
                    UserName = userName,
                    PictureHash = entry.Hash,
                    PictureData = entry.Data
                });
            }
            resp.Pictures = pics;
            return resp;
        }
    }
}
