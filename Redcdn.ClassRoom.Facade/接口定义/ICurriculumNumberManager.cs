using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redcdn.ClassRoom.Facade {
    public interface ICurriculumNumberManager
    {
        /// <summary>
        /// 获取所有课序
        /// </summary>
        /// <returns></returns>
        IList<CurriculumNumberDto> GetAll();

        /// <summary>
        /// 根据id获取课序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CurriculumNumberDto Get(int id);

        CurriculumNumberDto Create(CurriculumNumberDto entityDto);

        void Update(CurriculumNumberDto entityDto);

        void Delete(int id);

        PagingQueryResultDto<CurriculumNumberDto> GetList(ContentQueryPageParameter queryPagingParam);
    }
}
