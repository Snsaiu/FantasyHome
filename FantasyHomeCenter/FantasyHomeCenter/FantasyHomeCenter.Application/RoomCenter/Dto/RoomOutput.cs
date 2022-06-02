using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.RoomCenter.Dto;

public class RoomOutput
{
    [Display(Name="房间名")]
    public string RoomName { get; set; }
    public int Id { get; set; }
}