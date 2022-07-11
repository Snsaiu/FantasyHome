using FantasyHomeCenter.Application.ControlDeviceCenter.Dto;
using Microsoft.AspNetCore.Authorization;

namespace FantasyHomeCenter.Application.ControlDeviceCenter;

public class ControlDeviceService:IControlDeviceService,IDynamicApiController,ITransient
{
    [AllowAnonymous]
    public RESTfulResult<string> Regist(RegistMachineInput input)
    {
        return null;
    }
}