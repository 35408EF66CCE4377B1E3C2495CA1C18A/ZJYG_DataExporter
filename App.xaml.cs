using DevExpress.Xpf.Core;
using System.Windows;

namespace DataExporter
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            /*
            bool createdNew;
            Mutex instance = new Mutex(true, "ZJYG_ExportDataWindow", out createdNew);

            if (createdNew)
            {
                instance.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("程序已经启动，请等待！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(-1);
            }
             * */
          
            base.OnStartup(e);

            DXSplashScreen.Show<SplashScreenWindow1>();

        }
    }
}
