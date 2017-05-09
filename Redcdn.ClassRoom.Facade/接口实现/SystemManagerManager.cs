using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using GM.Orm;
using Redcdn.ClassRoom.Module;
using Redcdn.ClassRoom.Service;

namespace Redcdn.ClassRoom.Facade {
    public class SystemManagerManager:ISystemManager {
        public SystemManagerDto Login(string account, string pwd)
        {
            try{
                var dto = new SystemManagerDto() { SchoolManagerAccount = account, SchoolManagerPassWord = pwd };
                var module = SystemManagerService.Instance.Login(account, pwd);

                return AutoMapperWrapper.Instance.Map(module, dto);
            } 
            catch (BusinessException ex) {
                throw new BusinessException(-1, ex.Message);
            }
            catch (Exception ex){
                throw ex;
            }
        }

        public SystemManagerDto Create(SystemManagerDto entityDto)
        {
            var module = new SystemManager();
            var dto = new SystemManagerDto();
            try {
                AutoMapperWrapper.Instance.Map(entityDto, module);
                var entity=SystemManagerService.Instance.Create(module);
                return AutoMapperWrapper.Instance.Map(entity, dto);
            }
            catch (BusinessException ex) {
                throw new BusinessException(-1, ex.Message);
            }
            catch (Exception ex){
                throw ex;
            }
        }

        public void Update(SystemManagerDto entityDto)
        {
            var module = new SystemManager();
            try {
                AutoMapperWrapper.Instance.Map(entityDto, module);
                SystemManagerService.Instance.Update(module);
            } 
            catch (BusinessException ex) {
                throw new BusinessException(-1, ex.Message);
            }
            catch (Exception ex){
                throw ex;
            }
        }

        public SystemManagerDto GetSchoolManager()
        {
            var dto = new SystemManagerDto();
            try{
               var module= SystemManagerService.Instance.GetSchoolManager();
              return AutoMapperWrapper.Instance.Map(module, dto);
            } 
            catch (BusinessException ex) {
                throw new BusinessException(-1, ex.Message);
            }
            catch (Exception ex){
                throw ex;
            }
        }

        public SystemManagerDto GetById(int id)
        {
            var dto = new SystemManagerDto();
            try {
                var module = SystemManagerService.Instance.Get(new EntityKey(){Id = id});
                return AutoMapperWrapper.Instance.Map(module, dto);
            } catch (BusinessException ex) {
                throw new BusinessException(-1, ex.Message);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void ModifyPersonalInfo(int id, string nickname, string newpwd, string checknewpwd)
        {
            try{
                SystemManagerService.Instance.ModifyInfo(id, nickname, newpwd, checknewpwd);
            } 
            catch (BusinessException ex) {
                throw new BusinessException(-1, ex.Message);
            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
