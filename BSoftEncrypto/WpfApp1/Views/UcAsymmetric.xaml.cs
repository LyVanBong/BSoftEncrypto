using BSoftEncryptor.ViewModels;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BSoftEncryptor.UcAlgorithm
{
    /// <summary>
    /// Interaction logic for UcAsymmetric.xaml
    /// </summary>
    public partial class UcAsymmetric : UserControl
    {
        public UcAsymmetric()
        {
            InitializeComponent();
        }
        private string filePath;
        private AsymmetricAlgorithms rsa;
        private int checks = 0;

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFile.ShowDialog() == true)
            {
                filePath = openFile.FileName;
                txtFileOpen.Text = filePath;
                btnGenerateKey.IsEnabled = true;
                gbFeatures.IsEnabled = true;
            }
        }
        private void BtnGenerateKey_Click(object sender, RoutedEventArgs e)
        {
            string pathKey;
            rsa = new AsymmetricAlgorithms(filePath);

            var saveFoder = new System.Windows.Forms.FolderBrowserDialog();
            saveFoder.ShowDialog();
            pathKey = saveFoder.SelectedPath;
            txtKeys.Text = rsa.AssignNewKey(pathKey);
            MessageBox.Show("thanh cong");
        }

        private void RdEncryptor_Checked(object sender, RoutedEventArgs e)
        {
            if (checks > 0)
            {
                lbTypeKeys.Content = "PUBLIC KEY";
                btnEncryOrDecry.Content = "ENCRYPTION";
                btnGenerateKey.IsEnabled = true;
                keys.IsEnabled = false;
                checks++;
            }
        }

        private void RdDecryptor_Checked(object sender, RoutedEventArgs e)
        {
            lbTypeKeys.Content = "PRIVATE KEY";
            btnEncryOrDecry.Content = "DECRYPTION";
            btnGenerateKey.IsEnabled = false;
            keys.IsEnabled = true;
            txtKeys.Clear();
            checks++;
        }

        private void BtnKeys_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtKeys.Text = openFileDialog.FileName;
            }
        }

        private void BtnEncryOrDecry_Click_1(object sender, RoutedEventArgs e)
        {
            if (rdEncryptor.IsChecked == true)
            {
                try
                {
                    string a = txtKeys.Text;
                    string b = txtFileOpen.Text;
                    rsa = new AsymmetricAlgorithms(b);
                    rsa.EncryptData(a);
                    MessageBox.Show("thanh cong");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex);
                }
            }
            else
            {
                try
                {
                    string aa = txtFileOpen.Text;
                    string a = txtKeys.Text;

                    rsa = new AsymmetricAlgorithms(aa);
                    rsa.DecryptData(a);
                    MessageBox.Show("thanh cong");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex);
                }
            }
        }
    }
}
