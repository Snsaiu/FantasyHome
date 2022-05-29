// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using FantasyHomeCenter.UserCenter.Impl.Domains;
using FantasyHomeCenter.UserCenter.Dtos;
using Microsoft.AspNetCore.Mvc;
using FantasyHomeCenter.UserCenter.Services;
using Gardener;

namespace FantasyHomeCenter.UserCenter.Impl.Services
{
    /// <summary>
    /// 岗位管理服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class PositionService : ServiceBase<Position, PositionDto, int>, IPositionService
    {

        private readonly IRepository<Position> _positionRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionRepository"></param>
        public PositionService(IRepository<Position> positionRepository) : base(positionRepository)
        {
            this._positionRepository = positionRepository;
        }

    }
}
