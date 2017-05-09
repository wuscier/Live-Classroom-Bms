using System;
using System.Collections.Generic;
using System.Text;
using GM.Business.Module;
using GM.Orm;
using GM.Services;
using GM.Utilities;

namespace GM.Service.InterfaceServerControl
{
    public class FileUploadManager
    {
        private readonly static FileUploadManager _instance=new FileUploadManager();

        public static FileUploadManager Instance{get { return _instance; }}

        private FileUploadManager()
        {
            
        }

        public FileDeleteReply DeleteFiles(FileUploadReplyParam replyParam)
        {
            var reply = new FileDeleteReply() {ResultCode = 0, Desc = "批量删除文件成功"};
            try
            {
                using (DbTranscationScope scope = new DbTranscationScope())
                {
                    foreach (string cid in replyParam.ContentIds)
                    {
                        string pid = GetProviderbyCmsid(replyParam.DomainName);
                        RemoveDbRecord(string.Format("{0}/{1}", pid, cid));
                    }

                    scope.Complete();
                }

                Logger.WriteDebugingFmt(Log.DeleteFiles, "删除文件【cid:{0}】成功", string.Join("|", new List<string>(replyParam.ContentIds).ConvertAll(c => { return c.ToString(); }).ToArray()));
            }
            catch (Exception ex)
            {
                reply.ResultCode = -99;
                reply.Desc = ex.Message;
                Logger.WriteErrorFmt(Log.DeleteFiles, ex, "批量删除文件失败,【domainname:{0},filecids:{1}】", replyParam.DomainName, string.Join("|", new List<string>(replyParam.ContentIds).ConvertAll(file => file.ToString()).ToArray()));
            }
            return reply;
        }

        private void RemoveDbRecord(string outerId)
        {
            var ens = new Entities<PhysicalFileIM>("outer_id='{0}'", outerId).Cache;
            if (ens.Count == 0)
                throw new Exception("文件不存在或已删除");

            new Entities<PhysicalFileIM>().Remove(ens[0]);
        }

        private string GetProviderbyCmsid(string domainName)
        {
            var ens = new Entities<MediaService>("playback_url_prefix='{0}'", domainName).Cache;

            return ens.Count != 0 ? ens[0].ProviderID : null;
        }
    }
}
