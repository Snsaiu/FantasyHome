namespace FantasyHomeCenter.Core.Entities;

[Description("家人")]
public class Family:Entity
{

    public Family()
    {
        this.CreatedTime = DateTime.Now;
    }
    
    [Description("家人姓名")]
    [Required]
    public string UserName { get; set; }

    [Description("家人联系号码")]
    public string PhoneNumber { get; set; }
    
    [Required]
    [Description("家人密码")]
    public string Password { get; set; }
    
    [Description("家人拥有的设备")]
    public virtual ICollection<UiDevice> UiDevices { get; set; }
}