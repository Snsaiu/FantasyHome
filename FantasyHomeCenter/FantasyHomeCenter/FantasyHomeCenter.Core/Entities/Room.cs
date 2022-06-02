namespace FantasyHomeCenter.Core.Entities;

[Description("房间")]
public class Room:Entity
{
    public Room()
    {
        this.CreatedTime = DateTime.Now;
    }

    [Description("房间名称")]
    [Required]
    public string RoomName { get; set; }
    
}