using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {
    public class ContentQueryPageParameter {

        public List<string> OrderFields { get; set; }

        /// <summary>
        /// true 降序
        /// false 升序
        /// </summary>
        public Boolean OrderFlag { get; set; }

        public int PageSize { get; set; }

        public int StartIndex { get; set; }

    }
}
