using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.RoomCenter.Dto;

namespace FantasyHomeCenter.Application.RoomCenter;

public interface IRoomService
{
    /// <summary>
    /// 获得房间分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<RESTfulResult<PagedList<RoomOutput>>> GetRoomListPageAsync(RoomInput input);
    /// <summary>
    /// 添加新的房间
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<RESTfulResult<RoomOutput>> AddNewRoomAsync(AddRoomInput input);

    /// <summary>
    /// 批量删除房间
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<RESTfulResult<int>> DeleteRoomListAsync(List<int> ids);

    /// <summary>
    /// 获得所有的房间名
    /// </summary>
    /// <returns></returns>
    Task<RESTfulResult<List<RoomOutput>>> GetListAsync();
}