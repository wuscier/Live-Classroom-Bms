using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Redcdn.ClassRoom.Module;

namespace Redcdn.ClassRoom.Facade {
    public class AutoMapperWrapper {

        public static readonly AutoMapperWrapper _instance=new AutoMapperWrapper();
        bool _blInit;

        public static AutoMapperWrapper Instance {
            get { return _instance; }
        }

        private AutoMapperWrapper()
        {
            CustomMappingConfig();
        }

        public T2 Map<T1, T2>(T1 source, T2 dest) where T1 : class where T2 : class {
            try
            {
                Mapper.CreateMap<T1, T2>();
                return Mapper.Map<T1, T2>(source, dest);
            } 
            catch (Exception ex) {
                throw new Exception(string.Format("source:{0},dest:{1} 初始化映射失败,ex:{2}", source.GetType().Name, dest.GetType().Name, ex));
            }
        }

        /// <summary>
        /// 如业务中需要自定义属性转换，可自己增加转换规则
        /// eg:http://www.cnblogs.com/youring2/p/automapper.html
        /// </summary>
        private void CustomMappingConfig()
        {
            //Mapper.CreateMap<CurriculumManagerDto, Curriculum>()
            //.ForMember(dest => dest.CurriculumNameId, opt => opt.MapFrom(src => src.CurriculumNameId));
        }
    }
}
