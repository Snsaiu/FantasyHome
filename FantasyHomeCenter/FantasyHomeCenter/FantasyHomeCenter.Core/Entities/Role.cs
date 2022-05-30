using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Furion.DatabaseAccessor;

namespace FantasyHomeCenter.Core.Entities;

[Description("角色")]
public class Role:Entity
{
    public Role()
    {
        this.CreatedTime=DateTimeOffset.Now;
        this.Users = new List<User>();
    }

    public virtual ICollection<User> Users { get; set; }
    
    [Description("角色名称")]
    [Required]
    [MaxLength(32)]
    public string RoleName { get; set; }
}