using FantasyHomeCenter.Core.Enums;

namespace FantasyHomeCenter.Core.Entities;


public class CommandConstParams : Entity
{

    public CommandConstParams()
    {
        CreatedTime = DateTime.Now;
    }

    [Required]
    public string Name { get; set; }

    public string Value { get; set; }

    [Required]
    public CommandParameterType Type { get; set; }

    public Device Device{ get; set; }
}