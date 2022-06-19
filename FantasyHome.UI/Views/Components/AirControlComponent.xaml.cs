using System.Windows.Input;

namespace FantasyHome.UI.Views.Components;

public partial class AirControlComponent : ContentView
{

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
		set{SetValue(WindSpeedProperty,value);}
	}

	/// <summary>
	/// 开关状态
	/// </summary>
	public static readonly BindableProperty SwitchProperty = BindableProperty.Create(
		"Switch",
		typeof(bool),
		typeof(AirControlComponent),
		false);

	public bool Switch
	{
		get { return (bool)GetValue(SwitchProperty); }
		set{SetValue(SwitchProperty,value);}
	}
	
	/// <summary>
	/// 空调运行状态
	/// </summary>
	public static readonly BindableProperty WorkModeProperty=BindableProperty.Create(
		"WorkMode",
		typeof(WorkModeEnum),
		typeof(AirControlComponent),
		WorkModeEnum.自动
		);

	public WorkModeEnum WorkMode
	{
		get { return (WorkModeEnum)GetValue(WorkModeProperty); }
		set{SetValue(WorkModeProperty,value);}
	}
	
	/// <summary>
	/// 空调显示的名称
	/// </summary>
	public static  BindableProperty DisplayNameProperty=BindableProperty.Create(
		"DisplayName",
		typeof(string),
		typeof(AirControlComponent),
		"");

	public string DisplayName
	{
		get { return (string)GetValue(DisplayNameProperty); }
		set{SetValue(DisplayNameProperty,value);}
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
		set{ SetValue(PowerLevelProperty,value);}
	}
	
	/// <summary>
	/// 扫风模式
	/// </summary>
	public static BindableProperty SweepModeProperty =BindableProperty.Create(
		"SweepMode",
		typeof(string),
		typeof(AirControlComponent),
		"无");

	public string SweepMode
	{
		get { return (string)GetValue(SweepModeProperty); }
		set{SetValue(SweepModeProperty,value);}
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
		set{SetValue(ValueProperty,value);}
	}
	
	/// <summary>
	/// 室内温度
	/// </summary>
	public static BindableProperty RoomTemperatureProperty=BindableProperty.Create(
		"RoomTemperature",
		typeof(string),
		typeof(AirControlComponent),
		""
		);

	public string RoomTemperature
	{
		get { return (string)GetValue(RoomTemperatureProperty); }
		set{SetValue(RoomTemperatureProperty,value);}
	}
	
	
	#endregion

	#region 命令

	/// <summary>
	/// 改变风速的命令
	/// </summary>
	public static BindableProperty ChangeWindSpeedCommandProperty=BindableProperty.Create(
		"ChangeWindSpeedCommand",
		typeof(ICommand),
		typeof(AirControlComponent)
		);

	public ICommand ChangeWindSpeedCommand
	{
		get { return (ICommand)GetValue(ChangeWindSpeedCommandProperty); }
		set{SetValue(ChangeWindSpeedCommandProperty,value);}
	}
	
	/// <summary>
	/// 改变能耗的命令
	/// </summary>
	public static BindableProperty ChangePowerLevelCommandProperty=BindableProperty.Create(
		"ChangePowerLevelCommand",
		typeof(ICommand),
		typeof(AirControlComponent)
	);

	public ICommand ChangePowerLevelCommand
	{
		get { return (ICommand)GetValue(ChangePowerLevelCommandProperty); }
		set{SetValue(ChangePowerLevelCommandProperty,value);}
	}

	#endregion

	/// <summary>
	/// 风速点击弹框
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	/// <exception cref="NotImplementedException"></exception>
	private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
	{
	string data=	await Shell.Current.DisplayActionSheet("设置风速", "取消", "确定", "10", "20", "30", "40", "50", "60", "70", "80", "90",
			"100", "auto");
	if (string.IsNullOrEmpty(data) == false)
	{
		this.WindSpeed = data;
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
		string data=	await Shell.Current.DisplayActionSheet("设置能耗模式", "取消", "确定","节能","强劲","无");
		if (string.IsNullOrEmpty(data) == false)
		{
			this.PowerLevel = data;
		}
	}



	[ICommand]
    private void ChangeMethod(object obj)
    {

	    var item = obj as SwitchComponent;
	   
	    this.switchComponents.ForEach(x=>x.State=false);
	    this.switchComponents.First(x => x.Title == item.Title).State = true;


    }

}

public enum WorkModeEnum
{
	自动,制冷,抽湿,制热,送风
}
