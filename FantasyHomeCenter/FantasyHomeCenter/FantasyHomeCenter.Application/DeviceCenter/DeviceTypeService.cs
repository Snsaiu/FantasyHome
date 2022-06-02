using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using FantasyHomeCenter.Core.Entities;
using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Furion.UnifyResult;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FantasyHomeCenter.Application.DeviceCenter;

public class DeviceTypeService:IDynamicApiController,ITransient,IDeviceTypeService
{
    private readonly IRepository<DeviceType> repository;

    public DeviceTypeService(IRepository<DeviceType> repository)
    {
        this.repository = repository;
    }
    public async Task<RESTfulResult<DeviceTypeOutput>> AddDeviceTypeAsync(AddDeviceTypeInput input)
    {
        bool exist= await  this.repository.AnyAsync(x => x.DeviceTypeName == input.DeviceTypeName);
         if (exist)
             return new RESTfulResult<DeviceTypeOutput>() { Succeeded = false, Errors = $"{input.DeviceTypeName}已经存在" };
         var devicetype= input.Adapt<DeviceType>();
         var res= await this.repository.InsertNowAsync(devicetype);
         if (res == null)
             Oops.Bah($"插入{input.DeviceTypeName}失败");
         var convert_res = res.Adapt<DeviceTypeOutput>();
         return new RESTfulResult<DeviceTypeOutput>() { Succeeded = true, Data = convert_res };
    }

    public async Task<RESTfulResult<PagedList<DeviceTypeOutput>>> GetListPageAsync(DeviceTypeInput input)
    {
        var where = this.repository.AsQueryable();
       var res=   await where.Select(x => new DeviceTypeOutput()
        {
            DeviceTypeName = x.DeviceTypeName,
            Id = x.Id
        }).ToPagedListAsync();
       return new RESTfulResult<PagedList<DeviceTypeOutput>>() { Succeeded = true, Data = res };
    }

    public async Task<RESTfulResult<int>> DeleteDeviceTypeList(List<int> inputs)
    {
        //未来需要判断如果类型中还有设备，那么不应该被删除
        var list= await this.repository.AsQueryable().Where(x => inputs.Any(y => y == x.Id)).ToListAsync();
        await this.repository.DeleteNowAsync(list);
        return new RESTfulResult<int>() { Succeeded = true };
    }

    public async Task<RESTfulResult<List<DeviceTypeOutput>>> GetListAsync()
    {
        var entities= await this.repository.Entities.ToListAsync();
        var res= entities.Adapt<List<DeviceTypeOutput>>();
        return new RESTfulResult<List<DeviceTypeOutput>>() { Succeeded = true, Data = res };
    }
}