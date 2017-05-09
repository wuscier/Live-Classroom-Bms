using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using GM.Business.Module;
using GM.Orm;
using GM.Utilities;

namespace RedCdn.ClassRoom.BMS.Services {
    public class TelecomDomainDictionaryService:SingletonBase<TelecomDomainDictionaryService>
    {
        /// <summary>
        /// 将domain转换为大端序后，进行拆分。拆分出的servicedomain和protocoltype以小端序返回
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public TelecomDomainDictionary Build(int domain) {
            var buffer = ToBigEndBytes(domain);

            var serviceDomainBuffer = new byte[4];
            //将buffer从高位到低位的三个字节拷贝到一个四字节的int类型，以便获取一个完整的int值
            //如果buffer为xx xx xx 00，则把xx xx xx拷贝到serviceDomainBuffer（serviceDomainBuffer索引从1开始），作为serviceDomain的内存值
            Array.Copy(buffer, 0, serviceDomainBuffer, 1, 3);

            //将大端序转换为小端序
            var serviceDomain = ToLittlieEndInt(serviceDomainBuffer);
            //将buffer中xx xx xx 00最低位字节00赋值给protocolType
            var protocolType = buffer[3];

            return new TelecomDomainDictionary() { Domain = domain, ServiceDomain = serviceDomain, ProtocolType = protocolType };
        }

        /// <summary>
        /// 将servicedomain和protocoltype转换为大端序，合并成domain，再将domain转换为小端序输出
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public TelecomDomainDictionary Build(int servicedomain, int protocoltype) {
            var buffer = new byte[4];

            var serviceDomainBuffer = ToBigEndBytes(servicedomain);
            var protocolTypeBuffer = ToBigEndBytes(protocoltype);

            //将buffer填充字节。将serviceDomainBuffer索引1开始的3个字节，拷贝到buffer（buffer索引从0开始）
            Array.Copy(serviceDomainBuffer, 1, buffer, 0, 3);
            buffer[3] = protocolTypeBuffer[3];

            //通过buffer获取int类型，由于buffer是大端序，需要转换为小端序处理
            var domain = ToLittlieEndInt(buffer);
            return new TelecomDomainDictionary() { Domain = domain, ServiceDomain = servicedomain, ProtocolType = protocoltype };
        }

        /// <summary>
        /// 两个区域是否存在交集
        /// </summary>
        /// <param name="domaina"></param>
        /// <param name="domainb"></param>
        /// <param name="compareflag"></param>
        /// <returns></returns>
        public bool IsExistIntersection(TelecomDomainDictionary domaina, TelecomDomainDictionary domainb, TelecomDomainIntersectionFlag compareflag) {
            var blnServiceDomain = (domainb.ServiceDomain & domaina.ServiceDomain) > 0;
            var blnProtocolType = (domainb.ProtocolType & domaina.ProtocolType) > 0;

            if (compareflag == (TelecomDomainIntersectionFlag.ProtocolType | TelecomDomainIntersectionFlag.ServiceDomain))
                return blnProtocolType && blnServiceDomain;
            else if (compareflag == TelecomDomainIntersectionFlag.ProtocolType)
                return blnProtocolType;
            else if (compareflag == TelecomDomainIntersectionFlag.ServiceDomain)
                return blnServiceDomain;

            return false;
        }

        public TelecomDomainDictionary Get(int domain) {
            var cache = new Entities<TelecomDomainDictionary>("domain='{0}'", domain).Cache;
            return cache.Count == 0 ? null : cache[0];
        }

        //public TelecomDomainDictionary TryGetOrCreate(int domain) {
        //    var checkDomain = Get(domain);
        //    if (checkDomain == null)
        //        checkDomain = Create(Build(domain));

        //    return checkDomain;
        //}

        //将小端序转换为大端序 01 00 00 00-->00 00 00 01 （以数字1作为示例）
        byte[] ToBigEndBytes(int value) {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
        }

        //将大端序字节转换为小端序int值
        int ToLittlieEndInt(byte[] buffer) {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
        }
    }

    [Flags]
    public enum TelecomDomainIntersectionFlag {
        ServiceDomain = 1 << 0,
        ProtocolType = 1 << 1
    }
}