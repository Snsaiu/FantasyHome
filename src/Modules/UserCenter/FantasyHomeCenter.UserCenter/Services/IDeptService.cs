// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.UserCenter.Dtos;
using FantasyHomeCenter.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyHomeCenter.UserCenter.Services
{
    /// <summary>
    /// 部门服务
    /// </summary>
    public interface IDeptService : IServiceBase<DeptDto, int>
    {
        /// <summary>
        /// 查询所有部门 按树形结构返回
        /// </summary>
        /// <returns></returns>
        Task<List<DeptDto>> GetTree();

        /// <summary>
        /// 获取资源的种子数据
        /// </summary>
        /// <returns></returns>
        Task<string> GetSeedData();
    }
}
