using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {

    /// <summary>
    /// 系统管理接口
    /// </summary>
    public interface ISystemManager
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        SystemManagerDto Login(string account, string pwd);

       /// <summary>
       /// 创建校园管理员帐号
       /// </summary>
       /// <param name="entityDto"></param>
       /// <returns></returns>
        SystemManagerDto Create(SystemManagerDto entityDto);

        /// <summary>
        /// 更新校园管理员帐号信息
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        void Update(SystemManagerDto entityDto);

        /// <summary>
        /// 获取校园管理员帐号信息，一所学校只有一个校园管理员帐号
        /// </summary>
        /// <returns></returns>
        SystemManagerDto GetSchoolManager();

        /// <summary>
        /// 根据Id获取系统管理信息
        /// </summary>
        /// <returns></returns>
        SystemManagerDto GetById(int id);

        /// <summary>
        /// 修改人个信息
        /// </summary>
        /// <param name="id">主键id</param>
        /// <param name="nickName">显示名称</param>
        /// <param name="newpwd">新密码</param>
        /// <param name="checknewpwd">确认密码</param>
        void ModifyPersonalInfo(int id, string nickName, string newpwd, string checknewpwd);

    }
}
