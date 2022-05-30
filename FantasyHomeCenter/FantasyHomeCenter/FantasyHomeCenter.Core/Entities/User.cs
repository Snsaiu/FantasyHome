using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Core.Entities;

[Description("用户表")]
public class User:Entity
{

    public User()
    {
        this.CreatedTime=DateTime.Now;
        this.Roles = new List<Role>();

    }
    /// <summary>
    /// 姓名
    /// </summary>
    [MaxLength(32)]
    [Required]
    public string Name { get; set; }
    
    [Description("用户密码")]
    [Required]
    public string Password { get; set; }

    [Description("用户手机号码")]
    [Required]
    public string PhoneNumber { get; set; }

    public virtual  ICollection<Role> Roles{ get; set; }



}