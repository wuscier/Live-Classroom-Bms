using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using AutoMapper;
using GM.Utilities;
using Newtonsoft.Json;

namespace RedCdn.ClassRoom.BMS {
    public class CommonHelp : SingletonBase<CommonHelp>
    {
        public T ConvertToObj<T>(object obj) where T : class {
            return JsonConvert.DeserializeObject<T>(obj.ToString());
        }

        public string ConvertToJson(object obj) {
            var js = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

            return JsonConvert.SerializeObject(obj, js);
        }

        public T GetObjectByStream<T>(Stream stream) where T:class
        {
            var str = GetRequestBody(stream);
            Logger.WriteDebugingFmt(LogCatalogs.OperationHits, "body-data:{0}", str);
            return ConvertToObj<T>(str);
        }

        public  string GetRequestBody(Stream stream) {
            using (var reader = new StreamReader(stream, Encoding.UTF8)) {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 计算两个时间相差多少分钟
        /// </summary>
        /// <param name="dt1">开始时间</param>
        /// <param name="dt2">结束时间</param>
        /// <returns></returns>
        public int DataDiff(DateTime dt1,DateTime dt2)
        {
            TimeSpan ts= dt2.Subtract(dt1);
            return (int)ts.TotalSeconds;
        }

        public T2 Map<T1, T2>(T1 source, T2 dest)
            where T1 : class
            where T2 : class {
            try {
                Mapper.CreateMap<T1, T2>();
                return Mapper.Map<T1, T2>(source, dest);
            } catch (Exception ex) {
                throw new Exception(string.Format("source:{0},dest:{1} 映射转换失败,ex:{2}", source.GetType().Name, dest.GetType().Name, ex));
            }
        }
    }
}