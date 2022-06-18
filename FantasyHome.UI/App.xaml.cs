namespace FantasyHome.UI
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App(AppShell shell)
        {
            InitializeComponent();

            MainPage = shell; //new AppShell();
        }
    }
}