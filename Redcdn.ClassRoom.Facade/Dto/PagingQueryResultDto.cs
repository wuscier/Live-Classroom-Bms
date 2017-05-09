using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {
    public class PagingQueryResultDto<T> {

        public List<T> Result { get; set; }

        public int TotalCount { get; set; }

        public PagingQueryResultDto() {
            Result = new List<T>();
        }
    }
}
