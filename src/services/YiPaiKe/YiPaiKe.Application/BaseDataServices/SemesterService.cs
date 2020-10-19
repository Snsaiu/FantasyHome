﻿// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using YiPaiKe.Core.Entities;

namespace YiPaiKe.Application
{
    /// <summary>
    /// 学期服务
    /// </summary>
    [ApiDescriptionSettings("BaseDataServices")]
    public class SemesterService : ServiceBase<Semester>
    {
        /// <summary>
        /// 老师服务
        /// </summary>
        /// <param name="repository"></param>
        public SemesterService(IRepository<Semester> repository) : base(repository)
        {
        }
    }
}