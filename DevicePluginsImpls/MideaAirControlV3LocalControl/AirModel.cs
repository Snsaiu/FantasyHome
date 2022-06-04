namespace MideaAirControlV3LocalControl;

public class AirModel
{
    public string id { get; set; }
    public string name { get; set; }
    public string power_state { get; set; }
    public string prompt_tone { get; set; }
    public string target_temperature { get; set; }
    public string operational_mode { get; set; }
    public string fan_speed { get; set; }
    public string swing_mode { get; set; }
    public string eco_mode { get; set; }
    public string turbo_mode { get; set; }
    public string indoor_temperature { get; set; }
    public string outdoor_temperature { get; set; }
}