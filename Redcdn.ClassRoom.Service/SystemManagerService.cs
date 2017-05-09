using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using GM.Orm;
using Redcdn.ClassRoom.Module;

namespace Redcdn.ClassRoom.Service {
    public class SystemManagerService : EntityService<EntityKey, SystemManager, SystemManagerService> {

        public SystemManager Login(string account, string pwd) {
            string md5Pwd = CommonHelp.Md5Pwd(pwd);
            var ens = new Entities<SystemManager>("schoolmanager_account='{0}' and schoolmanager_password='{1}'", account, md5Pwd).Cache;
            if (ens.Count == 0)
                throw new BusinessException(-1, "帐号不存在或密码不正确");

            return ens[0];
        }
        public override SystemManager Create(SystemManager entity)
        {
            SystemManager schoolmanager = null;

            if(CheckAcoutIsExist(entity.SchoolManagerAccount))
                throw new BusinessException(-1, string.Format("创建校园管理帐号[{0}]已经存在", entity.SchoolManagerAccount));

            entity.SchoolManagerPassWord = CommonHelp.Md5Pwd(entity.SchoolManagerPassWord);

            try{
                using (DbTranscationScope scope = new DbTranscationScope())
                {

                    entity.AccountType = 1;//校管理员帐号

                    schoolmanager = base.Create(entity);
                    CreateNumberPool(entity.NumberPool);

                    scope.Complete();
                }
            }
            catch (Exception ex){
                 throw ex;
            }
            
            return schoolmanager;
        }

        public override void Update(SystemManager entity)
        {
            var schoolmanager = GetSchoolManager();
            if(schoolmanager==null)
                throw new BusinessException(-1, "校园管理员帐号不存在");

            schoolmanager.SchoolManagerName = entity.SchoolManagerName;
            if (!string.IsNullOrEmpty(entity.SchoolManagerPassWord))
                schoolmanager.SchoolManagerPassWord = CommonHelp.Md5Pwd(entity.SchoolManagerPassWord);

            try {
                using (DbTranscationScope scope = new DbTranscationScope()) {
                    base.Update(schoolmanager);
                    CreateNumberPool(entity.NumberPool);

                    scope.Complete();
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void ModifyInfo(int id, string nickName, string newpwd, string checknewpwd)
        {
            var ens = Get(new EntityKey() { Id = id });
            if(ens==null)
                throw new BusinessException(-1, string.Format("根据[id:{0}]信息未找到帐号信息", id));

            //var oldmd5 = ens.SchoolManagerPassWord;
            //var compareOldMd5 = CommonHelp.Md5Pwd(oldpwd);
            //if (string.Compare(oldmd5, compareOldMd5,true)!=0)
            //    throw new BusinessException(-1, "原密码不正确");

            ens.SchoolManagerName = nickName;
            ens.SchoolManagerPassWord = CommonHelp.Md5Pwd(newpwd);

            (ens as IEntity).Update();
        }

        public SystemManager GetSystemManager(int id)
        {
            var ens = Get(new EntityKey() { Id = id });
            if (ens == null)
                throw new BusinessException(-1, string.Format("根据[id:{0}]信息未找到帐号信息", id));

            return ens;
        }

        public SystemManager GetSchoolManager()
        {
            var ens = new Entities<SystemManager>("account_type='{0}'", 1).Cache;
           // var ens = new Entities<SystemManager>().Cache;
            if (ens.Count > 0)
            {
                 var nums=NumberPoolService.Instance.GetAll();
                if (nums.Count > 0)
                {
                    var list = new List<string>();
                    foreach (var numberPool in nums)
                        list.Add(numberPool.ClassNo);

                    ens[0].NumberPool = list.ToArray();
                }
                else
                    ens[0].NumberPool=new string[0];

                return ens[0];
            }

            return null;
        }

        bool CheckAcoutIsExist(string account)
        {
            var ens = new Entities<SystemManager>("schoolmanager_account='{0}'", account).Cache;
            return ens.Count == 0 ? false : true;
        }


        List<string> RemoveEmptyEntries(string[] numberpool)
        {
            var list = new List<string>();
            foreach (string str in numberpool)
            {
                if(string.IsNullOrEmpty(str))
                    continue;

                list.Add(str);
            }

            return list;
        }

        private void CreateNumberPool(string[] numberpool)
        {
            var numberpools= NumberPoolService.Instance.GetAll();
            if (numberpools.Count > 0)
            {
                // 数据库原号码池
                var oldnumPools = numberpools.ConvertAll(n => n.ClassNo).ToList();
                //新提交号码池
                var newList = RemoveEmptyEntries(numberpool);

                //  获取老的号码池有，新的号码池没有数据
                if (oldnumPools.Except(newList).Count() > 0)
                {
                    var expectedListA = oldnumPools.Except(newList).ToList();
                    expectedListA.ForEach(n =>
                    {
                        var entity = NumberPoolService.Instance.GetNumberPoolByNum(n);
                        var classroom = SchoolRoomService.Instance.GetByNubeNumber(entity.ClassNo);
                        if (classroom==null)//号码未分配教室可删除
                             NumberPoolService.Instance.Delete(entity);
                    });
                }

                // 获取新的号码池有的,老的号码池没有的数据
                if (newList.Except(oldnumPools).Count() > 0)
                {
                    var expectedListB = newList.Except(oldnumPools).ToList();
                    expectedListB.ForEach(n =>
                    {
                        NumberPoolService.Instance.Create(new NumberPool() { ClassNo = n, IsAllot = false });
                    });
                }
            }
            else
            {
                foreach (string number in numberpool)
                {
                    NumberPoolService.Instance.Create(new NumberPool() { ClassNo = number, IsAllot = false });
                }
            }
        }


    }
}
