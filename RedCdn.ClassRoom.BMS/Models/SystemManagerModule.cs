using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Redcdn.ClassRoom.Facade;

namespace RedCdn.ClassRoom.BMS {
    public class SystemManagerModule {

        public int? Id { get; set; }

        [Required(ErrorMessage = "请输校园管理员账号")]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "帐号长度不能大于 32 位。")]
        public string SchoolManagerAccount { get; set; }

        [Required(ErrorMessage = "请输入校园管理员名称")]
        [StringLength(128, MinimumLength = 1, ErrorMessage = "名称不能大于 128 位。")]
        public string SchoolManagerName { get; set; }

        public string SchoolManagerPassWord { get; set; }

        public string NumberPools { get; set; }
    }


    public class NumberPoolsConverter : TypeConverter<SystemManagerDto, SystemManagerModule> {
        protected override SystemManagerModule ConvertCore(SystemManagerDto source) {
            return new SystemManagerModule()
            {
                Id =source.Id,
                SchoolManagerAccount=source.SchoolManagerAccount,
                SchoolManagerName=source.SchoolManagerName,
                NumberPools = string.Join("\n", new List<string>(source.NumberPool).ConvertAll(s=>s.ToString())),
            };
        }
    }  
}