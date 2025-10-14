using System.Windows;

namespace GreenStock
{
    public partial class App : Application
    {

        protected void AppStart(object sender, StartupEventArgs e)
        {
            var loginWindow = new LoginWindow();

            loginWindow.Show();

            loginWindow.IsVisibleChanged += (s, ev) =>
            {
                if (loginWindow.IsVisible == false)
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    loginWindow.Close();
                }
            };
        }
    }
}
