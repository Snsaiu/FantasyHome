namespace FantasyHomeCenter.Core.Entities;

[Description("交互设备类型")]
public class UiDeviceType:Entity
{
    public UiDeviceType()
    {
        this.CreatedTime = DateTime.Now;
    }

    [Required]
    [Description("交互设备类型名称")]
    public string TypeName { get; set; }
    
}