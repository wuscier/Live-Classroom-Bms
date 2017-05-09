using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Module {
    public class BusinessException:Exception {

        public int ErrCode { get; set; }

        public BusinessException(int errcode, string msg, Exception innerException) : base(msg, innerException)
        {
            ErrCode = errcode;
        }

        public BusinessException(int errcode, string msg) : this(errcode, msg, null)
        {
            ErrCode = errcode;
        }
    }
}
