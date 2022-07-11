using Microsoft.AspNetCore.Authorization;

namespace FantasyHomeCenter.Application.CommonCenter;

public class CommonService:ICommonService,IDynamicApiController,ITransient
{
    
    [AllowAnonymous]
    public RESTfulResult<string> GetTryTest()
    {
        return new RESTfulResult<string>() { Data = "连接成功!" };
    }
}