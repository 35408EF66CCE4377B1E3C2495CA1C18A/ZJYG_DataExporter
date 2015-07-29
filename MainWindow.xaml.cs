using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using System;
using System.Threading;
using System.Windows;

namespace DataExporter
{
    /// <summary>
    /// Interaction logic for TestDataExporter.xaml
    /// </summary>
    public partial class MainWindow : DXRibbonWindow
    {
        public MainWindow()
        {
            // 设置鼠标光标到等待模式
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.AppStarting;

            // 启动Splash窗口
            //SplashScreenWindow1 splashwin = new SplashScreenWindow1();
            //splashwin.SetProgressState(true);
            //splashwin.Show();

            this.Loaded += MainWindow_Loaded;

            InitializeComponent();
            try
            {
                // 初始化ViewModel
                DataExporter.Classes.DataExporterViewModel _vm = new Classes.DataExporterViewModel();
                this.DataContext = _vm;
            }
            catch(Exception ex)
            {
                DXSplashScreen.Close();

                // 该错误的引发通常是没有开启数据库导致
                // 或者数据库的IP地址、用户名、密码错误导致
                MessageBox.Show(ex.Message, "严重错误", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(-1);
            }
            finally
            {
                // 回复鼠标光标到默认模式
                System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;

                // 关闭Splash窗口
                //splashwin.SetProgressState(false);
                //splashwin.CloseSplashScreen();
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DXSplashScreen.Close();
            this.Activate();
        }
    }
}
