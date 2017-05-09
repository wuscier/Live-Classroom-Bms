using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GM.Utilities;

namespace RedCdn.ClassRoom.ServiceSettingManagerTool {
    public class CatalogManager : SingletonBase<CatalogManager>
    {

        public List<Catalog> InitMenu()
        {
            var list = new List<Catalog>();
            list.Add(new Catalog() { Id = 1, Name = "设置GmDeviceidKey" });
            list.Add(new Catalog() { Id = 2, Name = "ISA 存储基目录设置" });
            list.Add(new Catalog() { Id = 3, Name = "服务器工作参数配置" });
            list.Add(new Catalog() { Id = 4, Name = "中继资源管理" });

            return list;
        }
    }
}
