using BSoftEncryptor.ViewModels;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BSoftEncryptor.UcAlgorithm
{
    /// <summary>
    /// Interaction logic for UcSymmetric.xaml
    /// </summary>
    public partial class UcSymmetric : UserControl
    {
        public UcSymmetric()
        {
            InitializeComponent();
            txtCreateRandomPassword.Text = SymmetricAlgorithms.CreateRandomPassword();
        }
        private int checks = 0;
        private void BtnChooseFileSymmetricAlgorithm_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFile.ShowDialog() == true)
            {
                txtFileSymmetricAlgorithm.Text = openFile.FileName;
            }
        }

        private void BtnGeneratePass_Click(object sender, RoutedEventArgs e)
        {
            int lengths = 8;
            bool isCheckLength = int.TryParse(txtCreateRandomPassword.Text, out lengths);
            if ((txtCreateRandomPassword.Text != "" || txtCreateRandomPassword.Text != null) && isCheckLength == true)
            {
                txtCreateRandomPassword.Text = SymmetricAlgorithms.CreateRandomPassword(lengths);
            }
            else if ((txtCreateRandomPassword.Text != "" || txtCreateRandomPassword.Text != null) && isCheckLength == false)
            {
                txtCreateRandomPassword.Text = SymmetricAlgorithms.CreateRandomPassword();
            }
            else
            {
                txtCreateRandomPassword.Text = SymmetricAlgorithms.CreateRandomPassword();
            }
        }

        private void BtnEncryOrDecry_Click(object sender, RoutedEventArgs e)
        {
            SymmetricAlgorithms symm = new SymmetricAlgorithms(txtFileSymmetricAlgorithm.Text, txtCreateRandomPassword.Text);
            if (rdEncryptor.IsChecked == true)
            {
                if (rdAES.IsChecked == true)
                {
                    try
                    {
                        symm.EncryptSymmetric(0);
                        MessageBox.Show("ENCRYPTOR SUCCESS", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ENCRYPTOR FAIL: " + ex.Message, "FAIL", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else if (rdDES.IsChecked == true)
                {
                    try
                    {
                        symm.EncryptSymmetric(1);
                        MessageBox.Show("ENCRYPTOR SUCCESS", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ENCRYPTOR FAIL: " + ex.Message, "FAIL", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else if (rdRC2.IsChecked == true)
                {
                    try
                    {
                        symm.EncryptSymmetric(2);
                        MessageBox.Show("ENCRYPTOR SUCCESS", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ENCRYPTOR FAIL", "FAIL", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    try
                    {
                        symm.EncryptSymmetric(3);
                        MessageBox.Show("ENCRYPTOR SUCCESS", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ENCRYPTOR FAIL", "FAIL", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                if (rdAES.IsChecked == true)
                {
                    try
                    {
                        symm.DecryptSymmetric(0);
                        MessageBox.Show("DECRYPTOR SUCCESS", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("DECRYPTOR FAIL: " + ex.Message, "FAIL", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else if (rdDES.IsChecked == true)
                {
                    try
                    {
                        symm.DecryptSymmetric(1);
                        MessageBox.Show("DECRYPTOR SUCCESS", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("DECRYPTOR FAIL", "FAIL", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else if (rdRC2.IsChecked == true)
                {
                    try
                    {
                        symm.DecryptSymmetric(2);
                        MessageBox.Show("DECRYPTOR SUCCESS", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("DECRYPTOR FAIL", "FAIL", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    try
                    {
                        symm.DecryptSymmetric(3);
                        MessageBox.Show("DECRYPTOR SUCCESS", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("DECRYPTOR FAIL", "FAIL", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void RdEncryptor_Checked(object sender, RoutedEventArgs e)
        {

            if (checks > 0)
            {
                imgEncryOrDecry.Source = new BitmapImage(new Uri("../Images/key_64px.png", UriKind.Relative));
                txtbEncryOrDecry.Text = "ENCRYPTION";
                checks++;
            }
        }

        private void RdDecryptor_Checked(object sender, RoutedEventArgs e)
        {
            imgEncryOrDecry.Source = new BitmapImage(new Uri("..\\Images\\unlock50px.png", UriKind.Relative));
            txtbEncryOrDecry.Text = "DECRYPTION";
            checks++;
        }
    }
}
