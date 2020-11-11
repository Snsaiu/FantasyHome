﻿// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Gardener.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.Application
{
    /// <summary>
    /// 班级服务
    /// </summary>
    [ApiDescriptionSettings("BaseDataServices")]
    public class ClassesService : ServiceBase<Classes>
    {
        /// <summary>
        /// 班级服务
        /// </summary>
        /// <param name="repository"></param>
        public ClassesService(IRepository<Classes> repository) : base(repository)
        {
        }
    }
}
