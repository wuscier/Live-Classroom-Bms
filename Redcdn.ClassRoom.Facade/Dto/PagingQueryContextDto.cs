using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {
    class PagingQueryContextDto {

        /// <summary>
        /// 从startindex起，查询的元素个数
        /// </summary>
        public int QueryCount { get; set; }

        /// <summary>
        /// 查询元素起始位置
        /// </summary>
        public int PageIndex { get; set; }
    }
}
