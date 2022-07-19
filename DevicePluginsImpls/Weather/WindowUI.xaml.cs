using System.Collections.Generic;
using System.Windows.Controls;
using FantasyHomeCenter.DevicePluginInterface;

namespace Weather
{
    public partial class WindowUI : ControlUI
    {
        public WindowUI(DeviceMetaOutput initData) : base(initData)
        {
            InitializeComponent();
            WindowUIModel vm = new WindowUIModel();
            this.DataContext = vm;
        }

        public override Dictionary<string, string> BuildInitRequest()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState(Dictionary<string, string> data)
        {
            throw new System.NotImplementedException();
        }
    }
}