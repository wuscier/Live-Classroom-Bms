using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {
    public interface IGradeManager {

        /// <summary>
        /// 获取所有年级数据
        /// </summary>
        /// <returns></returns>
        IList<GradeDto> GetAll();

        GradeDto Get(int id);

        GradeDto Create(GradeDto entityDto);

        void Update(GradeDto entityDto);

        void Delete(int id);

        PagingQueryResultDto<GradeDto> GetList(ContentQueryPageParameter queryPagingParam);
    }
}
