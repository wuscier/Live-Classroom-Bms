using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Redcdn.ClassRoom.Service {
    public sealed class CommonHelp {

        /// <summary>
        /// 根据当前页面和条数返回偏移量数据
        /// </summary>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="queryCount">查询条数</param>
        /// <returns></returns>
        public static int OffSetCount(int pageIndex, int queryCount) {
            return pageIndex == 0 ? pageIndex : pageIndex * queryCount;
        }

        public static string Md5Pwd(string pwd){
            byte[] buffer = MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(pwd));

            StringBuilder sb = new StringBuilder();
            foreach (byte value in buffer)
                sb.AppendFormat("{0:X2}", value);

            return sb.ToString();
        }
    }
}
