﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DynamicApiController;
using Furion.RemoteRequest.Extensions;
using Furion.SpecificationDocument;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Common;
using Gardener.Enums;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Gardener.Application
{
    /// <summary>
    /// Swagger服务
    /// </summary>
    [ApiDescriptionSettings("SystemManagerServices")]
    public class SwaggerService : ISwaggerService, IDynamicApiController
    {


        /// <summary>
        /// 解析api json
        /// </summary>
        /// <remarks> swagger json 文件解析功能</remarks>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SwaggerModel> Analysis(string url)
        {
            url = HttpUtility.UrlDecode(url);

            var swaggerInfo = await url.GetAsAsync<SwaggerModel>();

            return swaggerInfo;
        }
        /// <summary>
        /// 获取哦 swagger 配置
        /// </summary>
        /// <remarks>
        /// 获取api分组设置
        /// </remarks>
        /// <returns></returns>
        public async Task<List<SwaggerSpecificationOpenApiInfoDto>> GetApiGroup()
        {
            // 载入配置
            SpecificationDocumentSettingsOptions options= App.GetOptions<SpecificationDocumentSettingsOptions>();
            if (options == null) return null;
            return options.GroupOpenApiInfos.Select(x => x.Adapt<SwaggerSpecificationOpenApiInfoDto>()).ToList();
        }
        /// <summary>
        /// 从json中获取function
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<List<FunctionDto>> GetFunctionsFromJson(string url)
        {
            List<FunctionDto> _functionDtos = new List<FunctionDto>();
            SwaggerModel swaggerModel = await Analysis(url);
            if (swaggerModel != null && swaggerModel.paths != null)
            {
                Dictionary<string, SwaggerTagInfo> tagMap = swaggerModel.tags.ToDictionary<SwaggerTagInfo, string>(x => x.name);
                foreach (var item in swaggerModel.paths)
                {
                    foreach (var m in item.Value)
                    {
                        string tags = m.Value.tags == null ? null : string.Join("_", m.Value.tags.Select(x => tagMap.ContainsKey(x) ? tagMap[x].description : x));
                        FunctionDto function = new FunctionDto()
                        {
                            Path = item.Key,
                            Method = (HttpMethodType)Enum.Parse(typeof(HttpMethodType), m.Key.ToUpper()),
                            Key = MD5Encryption.Encrypt(item.Key + m.Key.ToUpper()),
                            Summary = m.Value.summary,
                            Description = m.Value.description,
                            //Group = _selectedGroup.Title,
                            Service = tags,
                            IsLocked = false,
                            EnableAudit = true
                        };
                        if (HttpMethodType.GET.Equals(function.Method))
                        {
                            function.EnableAudit = false;
                        }
                        _functionDtos.Add(function);
                    }
                }
            }
            return _functionDtos;
        }
    }
}