using System.Collections.Generic;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MideaAirControlV3LocalControl
{


    public class WindSpeedItem
    {
        public WindSpeedItem(string content,bool ischecked)
        {
            this.Content = content;
            this.IsChecked = ischecked;
        }

        public WindSpeedItem()
        {
            
        }
        public string Content { get; set; }
        public bool IsChecked { get; set; }
    }
    
    [ObservableObject]
    public partial class ShowWindSpeedDialog : Window
    {
        public delegate void SelectedChangedDelegate(string content);

        public event SelectedChangedDelegate SelectedChangedEvent;

        [ICommand]
        private void SelectedChange(object obj)
        {
            
            string c = obj.ToString();
            if (this.SelectedChangedEvent != null)
                this.SelectedChangedEvent(c);
        }

        [ICommand]
        private void CloseDialog()
        {
            this.Close();
        }
        public ShowWindSpeedDialog(string content)
        {
            List<WindSpeedItem> list = new();
            list.Add(new WindSpeedItem("自动",content=="自动"?true:false));

            
            InitializeComponent();
            this.DataContext = this;
            for (int i = 0; i <= 100; i+=10)
            {
                bool c = content == i.ToString() ? true : false;
                WindSpeedItem item = new WindSpeedItem(i.ToString(), c);
                list.Add(item);
            }
            // list.Add(new WindSpeedItem("自动",false));

            this.listview.ItemsSource = list;
        }
    }
}