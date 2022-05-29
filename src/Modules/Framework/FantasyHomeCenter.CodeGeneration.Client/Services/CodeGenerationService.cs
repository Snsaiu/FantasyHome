﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.CodeGeneration.Dtos;
using FantasyHomeCenter.CodeGeneration.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Client.Services
{
    /// <summary>
    /// 代码生成
    /// </summary>
    [ScopedService]
    public class CodeGenerationService : ICodeGenerationService
    {
        private static readonly string controller = "code-generation";

        private readonly IApiCaller apiCaller;

        public CodeGenerationService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<EntityCodeGenerationSettingDto> GetEntityCodeGenerationSetting(string entityFullName)
        {
            return await apiCaller.GetAsync<EntityCodeGenerationSettingDto>($"{controller}/entity-code-generation-setting/{entityFullName}");
        }

        public async Task<List<EntityDefinitionDto>> GetEntityDefinitions()
        {
            return await apiCaller.GetAsync<List<EntityDefinitionDto>>($"{controller}/entity-definitions");
        }

        public async Task<bool> AddEntityCodeGenerationSetting(EntityCodeGenerationSettingDto settingDto)
        {
            return await apiCaller.PostAsync<EntityCodeGenerationSettingDto,bool>($"{controller}/entity-code-generation-setting",settingDto);
        }

        public async Task<bool> UpdateEntityCodeGenerationSetting(EntityCodeGenerationSettingDto settingDto)
        {
            return await apiCaller.PutAsync<EntityCodeGenerationSettingDto, bool>($"{controller}/entity-code-generation-setting", settingDto);
        }
    }
}
