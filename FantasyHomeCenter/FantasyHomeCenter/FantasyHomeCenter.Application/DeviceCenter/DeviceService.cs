
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using FantasyHomeCenter.Core.Entities;
using Furion.DatabaseAccessor;

namespace FantasyHomeCenter.Application.DeviceCenter;

public class DeviceService:IDynamicApiController,ITransient,IDeviceService
{
    private readonly IRepository<Device> repository;

    public DeviceService(IRepository<Device> repository)
    {
        this.repository = repository;
    }
    public async Task<PagedList<DeviceOutput>> GetDevices(DeviceInput input)
    {
        var where = this.repository.AsQueryable();
        if (input.deviceType != null)
        {
            where.Where(x => x.Type.Id == input.deviceType);
        }
        var res= await  where.Select(x => new DeviceOutput()
            {
                Address = x.Address,
                Id = x.Id, 
                Name = x.Name,
                Type = x.Type.DeviceTypeName,
                Description =x.Description, 
                DeviceTypeId = x.Type.Id,
                State = x.State
            })
            .ToPagedListAsync();
       return res;
    }
}