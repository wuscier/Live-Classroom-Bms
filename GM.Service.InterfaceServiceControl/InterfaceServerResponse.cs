using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Services
{

    [Serializable]
    public class CreateServiceConfigResponse
    {
        public string ResultCode { get; set; }

        public string Desc { get; set; }

        public string FileDomainName { get; set; }

        public string LiveDomainName { get; set; }
    }

    [Serializable]
    public class InterfaceServerResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public ResultInfo ResultInfo { get; set; }
    }

    [Serializable]
    public class ResponseStatus
    {
        public int ResponseCode { get; set; }

        public string Description { get; set; }

    }

    [Serializable]
    public class ResultInfo
    {
        public List<CacheContent> cacheContents { get; set; }
    }

    [Serializable]
    public class CacheContent
    {
        /// <summary>
        /// 内容请求标识
        /// </summary>
        public string ContentID { get; set; }

        //public DateTime CreateTime { get; set; }
        /// <summary>
        /// 以秒为单位，界面展示时转换为时分秒
        /// </summary>
        public long Duration { get; set; }

        /// <summary>
        /// 以字节（B）为单位，界面展现时以M为单位
        /// </summary>
        public long FileSize { get; set; }

        public long ID { get; set; }

        /// <summary>
        /// 以bps为单位
        /// </summary>
        public long Rate { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public class ErrorCode
    {
        public const int Success = 0;

        public const int ParameterError = -1;

        public const int OtherCode = -100;

    }

}
