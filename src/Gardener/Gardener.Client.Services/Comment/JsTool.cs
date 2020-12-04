﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public class JsTool
    {
        public JsTool(IJSRuntime js)
        {
            SessionStorage = new SessionStorage(js);
            Document = new Document(js);
        }
        /// <summary>
        /// session storage
        /// </summary>
        public SessionStorage SessionStorage { get; init; }
        /// <summary>
        /// 
        /// </summary>
        public Document Document { get; set; }
    }
    /// <summary>
    /// session storage
    /// </summary>
    public class SessionStorage
    {
        private readonly IJSRuntime js;
        public SessionStorage(IJSRuntime js)
        {
            this.js = js;
        }
        public async Task SetAsync(string key, string value)
        {
            await js.InvokeVoidAsync("sessionStorage.setItem", key, value);
        }
        public async Task<T> GetAsync<T>(string key)
        {
           return await js.InvokeAsync<T>("sessionStorage.getItem", key);
        }
        public async Task RemoveAsync(string key)
        {
            await js.InvokeVoidAsync("sessionStorage.removeItem", key);
        }
    }

    public class Document
    {
        private readonly IJSRuntime js;
        public Document(IJSRuntime js)
        {
            this.js = js;
        }

        public async Task SetTitle(string title)
        {
            await js.InvokeVoidAsync("document.setTitle", title);
        }
    }
}
