using BSoftEncryptor.UcAlgorithm;
using BSoftEncryptor.Views;
using System.Windows;
using WpfApp1.Class;
using WpfApp1.UserControl;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            App.Current.Shutdown();
        }

        private void BrdSagust_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void HashFunc_Click(object sender, RoutedEventArgs e)
        {
            UcAdd.AddUc(content_bcryptor, new UcHashFunction());
        }

        private void Symmetric_Click(object sender, RoutedEventArgs e)
        {
            UcAdd.AddUc(content_bcryptor, new UcSymmetric());
        }

        private void Asymmetric_Click(object sender, RoutedEventArgs e)
        {
            UcAdd.AddUc(content_bcryptor, new UcAsymmetric());
        }

        private void BtnMinsize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnMinMaxSize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                btnMinMaxSize.Content = "";
            }
            else
            {
                this.WindowState = WindowState.Normal;
                btnMinMaxSize.Content = "";
            }
        }

        private void ImgLogo2_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (imgLogo2.Width != 80)
            {
                GridLength grd = new GridLength(80, GridUnitType.Pixel);
                menus.Width = grd;
                lbLogo1.Visibility = Visibility.Hidden;
                lb_secret.Visibility = Visibility.Hidden;
                lb_public.Visibility = Visibility.Hidden;
                lb_hash.Visibility = Visibility.Hidden;
                lb_about.Visibility = Visibility.Hidden;
                lb_setting.Visibility = Visibility.Hidden;
                lbDigitalSignatures.Visibility = Visibility.Hidden;
                lbLogo1.Width = 0;
                imgLogo2.Width = 80;
            }
            else
            {
                GridLength grd = new GridLength(220, GridUnitType.Pixel);
                menus.Width = grd;
                lbLogo1.Visibility = Visibility.Visible;
                lb_secret.Visibility = Visibility.Visible;
                lb_public.Visibility = Visibility.Visible;
                lb_hash.Visibility = Visibility.Visible;
                lb_about.Visibility = Visibility.Visible;
                lb_setting.Visibility = Visibility.Visible;
                lbDigitalSignatures.Visibility = Visibility.Visible;
                lbLogo1.Width = 145;
                imgLogo2.Width = 100;
                imgLogo2.Height = 100;
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            UcAdd.AddUc(content_bcryptor, new UcSymmetric());
        }

        private void GenerateKey_Click(object sender, RoutedEventArgs e)
        {
            UcAdd.AddUc(content_bcryptor, new UcGenerateKeys());
        }

        private void BtnSignatures_Click(object sender, RoutedEventArgs e)
        {
            UcAdd.AddUc(content_bcryptor, new UcDigitalSignatures());
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            UcAdd.AddUc(content_bcryptor, new UcAbout());
        }
    }
}
