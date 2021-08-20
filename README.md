<p align="center"><img src="https://images.gitee.com/uploads/images/2020/1204/145903_cea2bf9d_302533.png" height="80"/></p>

## 项目介绍

园丁是基于 .net 5开发的后台管理系统，系统前后台分离，api 是基于Furion 框架开发，前端是基于ant-design-blazor开发，系统使用技术或框架较新，喜欢的请点点star :kissing_heart: 。
## 演示地址
请善待弱鸡 :two_hearts:[http://49os.com:1000](http://49os.com:1000)，初次加载较慢，客官先喝口水吧。 [备用](http://47.94.212.176:1000)
## 已有功能
- 权限控制
  - 登录验证
  - 客户端页面资源验证
  - api请求验证
- 部门管理
- 岗位管理
- 用户管理
- 角色管理
- 资源管理
- 附件管理
- 操作审计
- 数据审计
- 登录Token管理

## 项目特点
- 新：.Net5 、Blazor WebAssembly 、Furion ；全部新鲜。
- 简：仅实现管理系统需要的功能，没有多余（懒。。）

## 开始使用
1. 确保安装了.net 5 sdk，如果使用vs,确保是vs2019最新版
2. 打开 API.sln 设置Gardener.Web.Entry 为启动项目，F5启动接口
3. 打开 Client.sln 设置 Gardener.Client 为启动项目，F5启动Client或右击wwwroot在浏览器打开
4. 默认用户名密码 admin/admin、testuser/testuser

## 项目结构

```
+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
+公共层	+---Gardener.Enums              	--公共枚举层                             
+       +---Gardener.Common             	--公共扩展层                             
+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
+       +---Gardener.Client.Models      	--Client实体模型层（client独有）         
+Client	+---Gardener.Client.Services    	--Client业务服务层                      
+       +---Gardener.Client             	--Client页面层                          
+       +---Gardener.ClientHost         	--Client宿主                            
+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
+       +---Gardener.Application        	--API端业务层（业务实现）              
+       +---Gardener.Application.Dtos   	--API端业务数据模型层（提供给Client） 
+       +---Gardener.Application.Interfaces     --API端业务接口定义层（提供给Client）  
+       +---Gardener.Core               	--API项目核心层（项目架构）             
+ API	+---Gardener.Core.Entites       	--API项目ORM实体模型                    
+       +---Gardener.Database.Migrations	--API数据库迁移（EF迁移）               
+       +---Gardener.EntityFramwork.Core	--API项目ORM EF核心层                  
+       +---Gardener.Web.Core           	--API Web核心层（Web框架核心及配置）    
+       +---Gardener.Web.Entry          	--API Web 服务入口                      
++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
```


## 常见问题
- 如何切换数据库：项目ORM框架使用的是EF，默认已使用sqlite，切换其它数据库首先需要通过nuget安装ef对应的支持包到`Gardener.EntityFramwork.Core`（[看这里](https://dotnetchina.gitee.io/furion/docs/dbcontext-start#9011-%E5%AE%89%E8%A3%85%E5%AF%B9%E5%BA%94%E6%95%B0%E6%8D%AE%E5%BA%93%E5%8C%85)），然后需要调整以下文件中对应名称等
`Gardener.EntityFramwork.Core.DbContexts.GardenerDbContext`
`Gardener.EntityFramwork.Core/dbsettings.json`
`Gardener.EntityFramwork.Core/GardenerEntityFrameworkCoreStartup`
调整后开始迁移，设置Gardener.Web.Entry 为启动项目，打开 工具=> Nuget包管理器=> 程序包管理器控制台，控制台默认项目选 Gardener.Database.Migrations， 执行EF迁移命令`Add-Migration v0.0.1 -Context GardenerDbContext`，`Update-Database v0.0.1 -Context GardenerDbContext`即可。
- 开发页面时如何热更新：在Gardener.Client目录执行`dotnet watch run`(最新vs2019 Ctrl+f5 即可热更新)

## 界面展示

<img src="https://images.gitee.com/uploads/images/2021/0807/101651_4fed42b7_302533.png" width="260px"/>
<img src="https://images.gitee.com/uploads/images/2021/0807/101709_a9342323_302533.png" width="260px"/>
<img src="https://images.gitee.com/uploads/images/2021/0807/101720_47067234_302533.png" width="260px"/>
<img src="https://images.gitee.com/uploads/images/2021/0807/101731_7dd825d7_302533.png" width="260px"/>
<img src="https://images.gitee.com/uploads/images/2021/0807/101742_918c9d0a_302533.png" width="260px"/>

<img src="https://images.gitee.com/uploads/images/2021/0807/101926_87c9983b_302533.png" width="260px"/>
<img src="https://images.gitee.com/uploads/images/2021/0807/101940_8e831f99_302533.png" width="260px"/>
<img src="https://images.gitee.com/uploads/images/2021/0807/101951_6a928009_302533.png" width="260px"/>

## 贡献代码

感谢每一位为 Furion 贡献代码的朋友，欢迎大家提交 PR 或 Issue。
[![Giteye chart](https://chart.giteye.net/gitee/hgflydream/Gardener/PPVXK76M.png)](https://giteye.net/chart/PPVXK76M)

## 基情链接
👉 **[Furion](https://gitee.com/monksoul/Furion)**
👉 **[ant-design-blazor](https://github.com/ant-design-blazor/ant-design-blazor)**

## 跟上组织
<a target="_blank" href="https://qm.qq.com/cgi-bin/qm/qr?k=ILV3MBrcZtr4uUSsKa3njjnpBiUvT0xe&jump_from=webapi"><img border="0" src="//pub.idqqimg.com/wpa/images/group.png" alt="学习交流群" title="学习交流群"></a>
