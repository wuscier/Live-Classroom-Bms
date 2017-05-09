using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {
    public interface INumberPoolManager
    {
        /// <summary>
        /// 获取未被分配的号码池
        /// </summary>
        /// <returns></returns>
        List<NumberPoolDto> GetNotAllocatedNumPool();
    }
}
