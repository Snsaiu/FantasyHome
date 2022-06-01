using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.UserCenter.Dto;

public class LoginUserInput
{

    
    [Required(ErrorMessage = "用户名不能为空")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "密码不能为空")]
    public string Pwd { get; set; }
}