using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.RoomCenter.Dto;
using FantasyHomeCenter.Core.Entities;
using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FantasyHomeCenter.Application.RoomCenter;

public class RoomService : IDynamicApiController, ITransient, IRoomService
{
    private readonly IRepository<Room> repository;

    public RoomService(IRepository<Room> repository)
    {
        this.repository = repository;
    }

    public async Task<RESTfulResult<PagedList<RoomOutput>>> GetRoomListPageAsync(RoomInput input)
    {
        var pages = await this.repository.Entities.Select(x => new RoomOutput()
        {
            Id = x.Id,
            RoomName = x.RoomName
        }).ToPagedListAsync(input.PageIndex, input.PageSize);
        return new RESTfulResult<PagedList<RoomOutput>>() { Succeeded = true, Data = pages };
    }

    public async Task<RESTfulResult<RoomOutput>> AddNewRoomAsync(AddRoomInput input)
    {
        //判断是否存在
        bool exist = await this.repository.AnyAsync(x => x.RoomName == input.RoomName);
        if (exist)
            return new RESTfulResult<RoomOutput>() { Succeeded = false, Errors = $"房间名{input.RoomName}已经存在" };
        var entity = input.Adapt<Room>();
        var insertRes = await this.repository.InsertNowAsync(entity);
        if (insertRes.State == EntityState.Added)
            return new RESTfulResult<RoomOutput>() { Succeeded = true, Data = insertRes.Entity.Adapt<RoomOutput>() };
        throw Oops.Bah($"插入{input.RoomName}失败!");
    }

    public async Task<RESTfulResult<int>> DeleteRoomListAsync(List<int> ids)
    {
        var list= await this.repository.AsQueryable().Where(x => ids.Any(y => y == x.Id)).ToListAsync();
        await this.repository.DeleteNowAsync(list);
        return new RESTfulResult<int>() { Succeeded = true };
    }

    public async Task<RESTfulResult<List<RoomOutput>>> GetListAsync()
    {
        var entities= await this.repository.Entities.ToListAsync();
        var res= entities.Adapt<List<RoomOutput>>();
        return new RESTfulResult<List<RoomOutput>>() { Succeeded = true, Data = res };
    }
}