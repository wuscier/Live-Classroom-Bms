using System;
using System.Collections.Generic;
using System.Text;
using GM.Business.Service;
using Newtonsoft.Json;

namespace GM.Services
{
    public class CommonHelper:SingletonBase<CommonHelper>
    {
        public T ConvertToObj<T>(object obj) where T : class
        {
            return JsonConvert.DeserializeObject<T>(obj.ToString());
        }

        public string ConvertToJson(object obj)
        {
            var js = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

            return JsonConvert.SerializeObject(obj, js);
        }
    }
}
