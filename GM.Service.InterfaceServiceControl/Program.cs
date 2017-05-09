using System;
using System.Collections.Generic;
using System.Text;

using GM.Utilities.Web;
using GM.Utilities;

namespace GM.Services
{
    class Program
    {
        public static void  Main()
        {
            Logger.WriteInfo(Log.Debug, "以控制台启动");
            HttpMethodService m = new HttpMethodService();
            m.Start();
            
            Console.Read();
        }
    }
}
