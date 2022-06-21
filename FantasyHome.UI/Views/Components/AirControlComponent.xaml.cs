using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FantasyHome.UI.Views.Components;


public delegate void SendActionDelegate(AirModel model);

public partial class AirControlComponent : ContentView
{

    public event SendActionDelegate SendActionEvent;
    
    private List<SwitchComponent> switchComponents;

    public AirControlComponent()
    {
        InitializeComponent();
        this.BindingContext = this;
        this.switchComponents = new List<SwitchComponent>();
        this.switchComponents.Add(this.auto);
        this.switchComponents.Add(this.zhileng);
        this.switchComponents.Add(this.zhire);
        this.switchComponents.Add(this.songfeng);
        this.switchComponents.Add(this.choushi);
    }

    #region 属性

    /// <summary>
    /// 风速
    /// </summary>
    public static readonly BindableProperty WindSpeedProperty = BindableProperty.Create("WindSpeed",
        typeof(string), typeof(AirControlComponent), "auto");

    public string WindSpeed
    {
        get { return (string)GetValue(WindSpeedProperty); }
        set { SetValue(WindSpeedProperty, value); }
    }

    /// <summary>
    /// token
    /// </summary>
    public static readonly BindableProperty TokenProperty = BindableProperty.Create("Token",
        typeof(string), typeof(AirControlComponent), "");

    public string Token
    {
        get { return (string)GetValue(TokenProperty); }
        set { SetValue(TokenProperty, value); }
    }
    
    /// <summary>
    /// token
    /// </summary>
    public static readonly BindableProperty Ack1Property = BindableProperty.Create("Ack1",
        typeof(string), typeof(AirControlComponent), "");

    public string Ack1
    {
        get { return (string)GetValue(Ack1Property); }
        set { SetValue(Ack1Property, value); }
    }
    
    /// <summary>
    /// token
    /// </summary>
    public static readonly BindableProperty AcIpProperty = BindableProperty.Create("AcIp",
        typeof(string), typeof(AirControlComponent), "");

    public string AcIp
    {
        get { return (string)GetValue(AcIpProperty); }
        set { SetValue(AcIpProperty, value); }
    }

    
    /// <summary>
    /// token
    /// </summary>
    public static readonly BindableProperty AcIdProperty = BindableProperty.Create("AcId",
        typeof(string), typeof(AirControlComponent), "");

    public string AcId
    {
        get { return (string)GetValue(AcIdProperty); }
        set { SetValue(AcIdProperty, value); }
    }

    
    /// <summary>
    /// 装配数据
    /// </summary>
    /// <returns></returns>
    private void sendActionModel()
    {
        
        
        AirModel model = new AirModel();

        model.Ack1 = this.Ack1;
        model.AcId = this.AcId;
        model.Token = this.Token;
        model.AcIp = this.AcIp;
        model.CurrentTemperature = this.Value.ToString();
        model.PowerLevel = this.PowerLevel;
        model.WindSpeed = this.WindSpeed;
        model.SwitchState = this.AirSwitch;
        model.RunMode = this.switchComponents.First(x => x.State == true).Title;

        
        if (this.SendActionEvent!=null)
        {
            this.SendActionEvent(model);
        }
       

    }

    /// <summary>
    /// 开关状态
    /// </summary>
    public static readonly BindableProperty AirSwitchProperty = BindableProperty.Create(
        "AirSwitch",
        typeof(bool),
        typeof(AirControlComponent),
        false
    );

    public bool AirSwitch
    {
        get { return (bool)GetValue(AirSwitchProperty); }
        set { SetValue(AirSwitchProperty, value); }
    }

    /// <summary>
    /// 空调运行状态
    /// </summary>
    public static readonly BindableProperty WorkModeProperty = BindableProperty.Create(
        "WorkMode",
        typeof(WorkModeEnum),
        typeof(AirControlComponent),
        WorkModeEnum.自动
    );

    public WorkModeEnum WorkMode
    {
        get { return (WorkModeEnum)GetValue(WorkModeProperty); }
        set { SetValue(WorkModeProperty, value); }
    }

    /// <summary>
    /// 空调显示的名称
    /// </summary>
    public static BindableProperty DisplayNameProperty = BindableProperty.Create(
        "DisplayName",
        typeof(string),
        typeof(AirControlComponent),
        ""
    );

    public string DisplayName
    {
        get { return (string)GetValue(DisplayNameProperty); }
        set { SetValue(DisplayNameProperty, value); }
    }

    /// <summary>
    /// 能耗级别
    /// </summary>
    public static BindableProperty PowerLevelProperty = BindableProperty.Create(
        "PowerLevel",
        typeof(string),
        typeof(AirControlComponent),
        "无"
    );

    public string PowerLevel
    {
        get { return (string)GetValue(PowerLevelProperty); }
        set { SetValue(PowerLevelProperty, value); }
    }

    /// <summary>
    /// 扫风模式
    /// </summary>
    public static BindableProperty SweepModeProperty = BindableProperty.Create(
        "SweepMode",
        typeof(string),
        typeof(AirControlComponent),
        "无"
    );

    public string SweepMode
    {
        get { return (string)GetValue(SweepModeProperty); }
        set { SetValue(SweepModeProperty, value); }
    }

    /// <summary>
    /// 设置空调工作温度
    /// </summary>
    public static BindableProperty ValueProperty = BindableProperty.Create(
        "Value",
        typeof(int),
        typeof(AirControlComponent),
        26
    );

    public int Value
    {
        get { return (int)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }

    /// <summary>
    /// 室内温度
    /// </summary>
    public static BindableProperty RoomTemperatureProperty = BindableProperty.Create(
        "RoomTemperature",
        typeof(string),
        typeof(AirControlComponent),
        ""
    );

    public string RoomTemperature
    {
        get { return (string)GetValue(RoomTemperatureProperty); }
        set { SetValue(RoomTemperatureProperty, value); }
    }

    #endregion

    #region 命令

    
    #endregion

    /// <summary>
    /// 风速点击弹框
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
    {
        string data = await Shell.Current.DisplayActionSheet("设置风速", "取消", "确定", "10", "20", "30", "40", "50", "60",
            "70", "80", "90",
            "100", "auto");
        if (string.IsNullOrEmpty(data) == false)
        {
            if (data == "取消")
                return;
            if (data != "确定")
            {
                this.WindSpeed = data;
                this.sendActionModel();
            }
               
        }
    }

    /// <summary>
    /// 能耗选择
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private async void PowerLevel_OnTapped(object sender, EventArgs e)
    {
        string data = await Shell.Current.DisplayActionSheet("设置能耗模式", "取消", "确定", "节能", "强劲", "无");
        if (string.IsNullOrEmpty(data) == false)
        {
            if (data == "取消")
                return;
            if (data != "确定")
            {
                this.PowerLevel = data;
    
                this.sendActionModel();
             
            }
               
        }
    }


    /// <summary>
    /// 改变空调运行状态
    /// </summary>
    /// <param name="obj"></param>
    [ICommand]
    private void ChangeMethod(object obj)
    {
        var item = obj as SwitchComponent;

        this.switchComponents.ForEach(x => x.State = false);
        this.switchComponents.First(x => x.Title == item.Title).State = true;
        this.sendActionModel();
    }

    [ICommand]
    private void SetAirState(object obj)
    {
        this.AirSwitch = !this.AirSwitch;
        this.sendActionModel();
    }

    [ICommand]
    private void ValueChange()
    {
        this.sendActionModel();
    }
}
public class AirModel
{

    /// <summary>
    /// 空调ack1
    /// </summary>
    public string Ack1 { get; set; }
		
    /// <summary>
    /// 空调id
    /// </summary>
    public string AcId { get; set; }
		
    /// <summary>
    /// 空调ip
    /// </summary>
    public string AcIp { get; set; }

    /// <summary>
    /// 当前token
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// 当前温度
    /// </summary>
    public string CurrentTemperature { get; set; }
		
    /// <summary>
    /// 能耗级别
    /// </summary>
    public string PowerLevel { get; set; }
		
    /// <summary>
    /// 运行模式
    /// </summary>
    public string RunMode{ get; set; }
		
    /// <summary>
    /// 风速
    /// </summary>
    public string WindSpeed { get; set; }
		
    /// <summary>
    /// 开关状态
    /// </summary>
    public bool SwitchState { get; set; }
}

public enum WorkModeEnum
{
    自动,
    制冷,
    抽湿,
    制热,
    送风
}