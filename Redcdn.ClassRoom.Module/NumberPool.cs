using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Orm;

namespace Redcdn.ClassRoom.Module {

    /// <summary>
    /// 号码池
    /// </summary>
    [DBTable("number_pool")]
    public class NumberPool : Entity,IDeleteflag {

        /// <summary>
        /// 底层分配的教室号
        /// </summary>
        [DBField("classno")]
        public string ClassNo { get; set; }

        //号码是否被分配给其他教室
        [DBField("isallot")]
        public bool IsAllot { get; set; }

        [DBField("delete_flag")]
        public bool Deleteflag { get; set; }
    }
}
