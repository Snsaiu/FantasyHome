using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHome.Application.Dto;


namespace FantasyHome.Application
{

    public interface IRoomApplication
    {


        /// <summary>
        /// 获得房间
        /// </summary>
        /// <returns></returns>
        Task<ResultBase<List<RoomOutput>>> GetRooms();
    }
}