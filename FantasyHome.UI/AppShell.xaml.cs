using FantasyHome.Application;

namespace FantasyHome.UI
{
    public partial class AppShell : Shell
    {

     //   public ObservableCollection<dynamic> FlyoutItems { get; set; }
        public AppShell(AppShellModel appShellModel)
        {
           
            InitializeComponent();
            this.BindingContext = appShellModel;
 

        }

       
    }
}