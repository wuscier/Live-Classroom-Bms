using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;
using GM.Orm.Db;
using GM.Service.InterfaceServerControl;
using Redcdn.ClassRoom.Module;

namespace Redcdn.ClassRoom.Service
{
    public class RecordFileService : EntityService<EntityKey, RecordFile, RecordFileService>
    {
        private string cacheLrsIp = string.Empty;

        public override RecordFile Create(RecordFile entity)
        {
            // 文件序号
            int fileseq=1;
            var ens = new Entities<RecordFile>("Class_No='{0}' order by create_time desc",entity.ClassNo).Cache;
            if (ens.Count > 0) {
                ens[0].FileSequence++;
                entity.FileSequence = ens[0].FileSequence;
            } else {
                entity.FileSequence = fileseq;
            }
           
            return base.Create(entity);
        }

        public PagingQueryEntityResult<RecordFile> GetList(List<string> orderFields, bool orderFlag, int startIndex, int pageSize)
        {
            var context = new PagingQueryContext()
            {
                Order = orderFlag ? DbQueryOrderFlag.Desc : DbQueryOrderFlag.Asc,
                OrderFields = orderFields.ToArray(),
                StartIndex = CommonHelp.OffSetCount(startIndex, pageSize),
                QueryCount = pageSize
            };

            return new Entities<RecordFile>(context, "record_status=1").PagingCache;
        }

        public PagingQueryEntityResult<RecordFile> GetList(List<string> orderFields, bool orderFlag, int startIndex, int pageSize, RecordFile queryParam)
        {
            var context = new PagingQueryContext()
            {
                Order = orderFlag ? DbQueryOrderFlag.Desc : DbQueryOrderFlag.Asc,
                OrderFields = orderFields.ToArray(),
                StartIndex = CommonHelp.OffSetCount(startIndex, pageSize),
                QueryCount = pageSize
            };

            string whereSql = "1=1";

            if (queryParam.CurriculumId > 0)
                whereSql += string.Format(" and curriculum_id = {0} ", queryParam.CurriculumId);           

            return new Entities<RecordFile>(context, whereSql).PagingCache;
          
        }


        public PagingQueryEntityResult<RecordFile> GetListByClassroomId(List<string> orderFields, bool orderFlag, int startIndex, int pageSize,int classroomId) {
            var context = new PagingQueryContext() {
                Order = orderFlag ? DbQueryOrderFlag.Desc : DbQueryOrderFlag.Asc,
                OrderFields = orderFields.ToArray(),
                StartIndex = CommonHelp.OffSetCount(startIndex, pageSize),
                QueryCount = pageSize
            };

            string whereSql = string.Format("classroom_id = {0} ", classroomId);
            return new Entities<RecordFile>(context, whereSql).PagingCache;

        }


        public List<RecordFile> GetList(int recordId)
        {
            return new Entities<RecordFile>("record_id={0}", recordId).Cache;
        }

        public List<RecordFile> GetByCurriculumId(int curriculumId)
        {
            return new Entities<RecordFile>("Curriculum_Id={0}", curriculumId).Cache;
        }

        public List<RecordFile> GetByClassroomId(int classroomId) {
            return new Entities<RecordFile>("classroom_id={0}", classroomId).Cache;
        }

        string GetIesIp() {
            if (string.IsNullOrEmpty(cacheLrsIp)) {
                string sql = "SELECT outer_ip FROM equipment WHERE type_id=2";
                var ds = DB.DBManager.GetDs(null, sql);
                if (ds != null && ds.Tables.Count > 0)
                    return cacheLrsIp = ds.Tables[0].Rows[0][0].ToString();
            }
            return cacheLrsIp;
        }

        public void StartRecord(TerminalRecordParam param)
        {
            RecordManager.Instance.StartRecord(new StartRecordParam() {
                LiveChannelPid =param.ChannelOuterId.Split('/')[0],
                LiveChannelCid = param.ChannelOuterId.Split('/')[1],
                RecordFilePid = param.RecordFileOuterId.Split('/')[0],
                RecordFileCid = param.RecordFileOuterId.Split('/')[1],
                Context = param.Context,
                ServerIp = GetIesIp()// SettingConfig.Instance.LrsServerIp
            });
        }

        public void StopRecord(TerminalRecordParam param)
        {
            RecordManager.Instance.StopRecord(new StopRecordParam() {
                LivechannelPid = param.ChannelOuterId.Split('/')[0],
                LiveChannelCid = param.ChannelOuterId.Split('/')[1],
                ServerIp = GetIesIp()// SettingConfig.Instance.LrsServerIp
            });
        }
    }
}
