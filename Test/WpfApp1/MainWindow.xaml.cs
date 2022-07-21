using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        public ObservableCollection<Vm> Vms { get; set; }

        public MainWindow()
        {
         
          
            InitializeComponent();
            this.Vms = new ObservableCollection<Vm>();
            this.Vms.Add(new Vm { Icon = "./100.svg" ,Date="6-7"});
            this.Vms.Add(new Vm { Icon = "./100.svg", Date = "6-7" });
            this.DataContext = this.Vms;


            //  this.box.Source=new BitmapImage(new Uri("))

            //  this.box.Source = new Uri(icon, UriKind.Relative);
        }
    }

    [ObservableObject]
    public partial class Vm
    {

      

        [ObservableProperty]
        private string icon;
        [ObservableProperty]
        private string date;
    }
}
