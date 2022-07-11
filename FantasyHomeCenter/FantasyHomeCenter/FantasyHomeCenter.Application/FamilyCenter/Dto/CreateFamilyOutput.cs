using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.FamilyCenter.Dto;

public class CreateFamilyOutput
{
    
    public int Id { get; set; }
    
    [Display(Name = "家人姓名")]
    public string UserName { get; set; }

    [Display(Name = "家人联系号码")]
    public string PhoneNumber { get; set; }

}