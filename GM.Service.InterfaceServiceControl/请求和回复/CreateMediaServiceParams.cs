using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Service.InterfaceServerControl
{
    public class CreateMediaServiceParams
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 公众号对应的域名标识
        /// </summary>
        public string Alise { get; set; }
    }
}
