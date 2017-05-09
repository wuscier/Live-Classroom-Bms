using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Redcdn.ClassRoom.Module;
using Redcdn.ClassRoom.Service;

namespace Redcdn.ClassRoom.Facade {
    public class NumberPoolManager:INumberPoolManager {
        public List<NumberPoolDto> GetNotAllocatedNumPool()
        {
            var list = new List<NumberPoolDto>();
            try
            {
                var numpools= NumberPoolService.Instance.GetNotAllocatedNumPool();
                foreach (NumberPool module in numpools)
                {
                    var dto = new NumberPoolDto();
                    AutoMapperWrapper.Instance.Map(module, dto);
                    list.Add(dto);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
