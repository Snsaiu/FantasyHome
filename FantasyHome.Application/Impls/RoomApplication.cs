using FantasyHome.Application.Dto;


namespace FantasyHome.Application.Impls;

public class RoomApplication:IRoomApplication
{
    public Task<ResultBase<List<RoomOutput>>> GetRooms()
    {
        ResultBase<List<RoomOutput>> res = new ResultBase<List<RoomOutput>>();
        res.Data.Add(new RoomOutput(){Id = 1,RoomName = "客厅"});
        res.Data.Add(new RoomOutput(){Id = 2,RoomName = "次卧"});
        res.Data.Add(new RoomOutput(){Id = 3,RoomName = "主卧"});

        res.Succeeded = true;
        return Task.FromResult(res);

    }
}