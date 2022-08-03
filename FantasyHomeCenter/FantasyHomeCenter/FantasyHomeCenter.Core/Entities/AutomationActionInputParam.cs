namespace FantasyHomeCenter.Core.Entities;

public class AutomationActionInputParam:Entity
{


    public AutomationAction AutomationAction { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }


    public ActionValueType Type { get; set; }

}

public enum ActionValueType
{
    Get,Set
}