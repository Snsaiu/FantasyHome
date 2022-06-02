using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.RoomCenter.Dto;

public class AddRoomInput
{
    [Display(Name = "房间名")]
    [Required(ErrorMessage = "房间名不能为空")]
    public string RoomName { get; set; }
}