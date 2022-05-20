﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Email.Dtos;
using Gardener.Email.Services;
using Gardener.SysTimer.Dtos;
using Gardener.SysTimer.Services;
using System;
using System.Threading.Tasks;

namespace Gardener.SysTimer.Client.Services
{
    [ScopedService]
    public class SysTimerService : ClientServiceBase<SysTimerDto, int>, ISysTimerService
    {
        public SysTimerService(IApiCaller apiCaller) : base(apiCaller, "sys-timer")
        {

        }

        public void AddTimerJob(SysTimerDto input)
        {
            throw new NotImplementedException();
        }
        public async void Start(SysTimerDto input)
        {
            await apiCaller.PostAsync($"{controller}/start", input);
        }

        public void StartTimerJob()
        {
            throw new NotImplementedException();
        }

        public async void Stop(StopJobInput input)
        {
            await apiCaller.PostAsync($"{controller}/stop", input);
        }
    }
}