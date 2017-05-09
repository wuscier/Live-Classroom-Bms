using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {
    public interface ICurriculumNameManager {

        /// <summary>
        /// 获取所有课程名
        /// </summary>
        /// <returns></returns>
        IList<CurriculumNameDto> GetAll();

        CurriculumNameDto Get(int id);

        CurriculumNameDto Create(CurriculumNameDto entityDto);

        void Update(CurriculumNameDto entityDto);

        void Delete(int id);

        PagingQueryResultDto<CurriculumNameDto> GetList(ContentQueryPageParameter queryPagingParam);
    }
}
