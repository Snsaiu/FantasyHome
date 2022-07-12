using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.FamilyCenter.Dto;

public class CreateFamilyInput
{
    
    [Required(ErrorMessage = "家人称呼不能为空")]
    public string Name { get; set; }
    
    public string Phone { get; set; }
    
    [Required(ErrorMessage = "密码不能为空")]
    public string Pwd { get; set; }
}