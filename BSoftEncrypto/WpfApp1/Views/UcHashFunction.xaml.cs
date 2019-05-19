using BSoftEncryptor.Class;
using Microsoft.Win32;
using System;
using System.Windows;

namespace WpfApp1.UserControl
{
    /// <summary>
    /// Interaction logic for UcHashFunction.xaml
    /// </summary>
    public partial class UcHashFunction : System.Windows.Controls.UserControl
    {
        public UcHashFunction()
        {
            InitializeComponent();
        }
        HashFunction hash;
        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openfile.ShowDialog() == true)
            {
                hash = new HashFunction(openfile.FileName);
                txtPathNameFile.Text = openfile.FileName;
                txtPathNameFile.Tag = openfile.FileName;
                try
                {
                    txtMd5.Text = hash.gethashcodes(0);
                    txtSha1.Text = hash.gethashcodes(1);
                    txtSha256.Text = hash.gethashcodes(2);
                    txtSha512.Text = hash.gethashcodes(3);
                }
                catch (Exception)
                {
                    MessageBox.Show("File Size Very Large ( <2GB ) !", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtPathNameFile.Clear();
                }
            }
        }

        private void BtnVeryfy_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (hash.VeryfyHash(txtMd5.Text, txtVerifyHash.Text))
                {
                    MessageBox.Show("Veryfy MD5 successful", "successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (hash.VeryfyHash(txtSha1.Text, txtVerifyHash.Text))
                {
                    MessageBox.Show("Veryfy SHA-1 successful", "successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (hash.VeryfyHash(txtSha256.Text, txtVerifyHash.Text))
                {
                    MessageBox.Show("Veryfy SHA-256 successful", "successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (hash.VeryfyHash(txtSha512.Text, txtVerifyHash.Text))
                {
                    MessageBox.Show("Veryfy SHA-512 successful", "successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Veryfy Hash Code Fail", "Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
            catch (Exception)
            {
                MessageBox.Show($"Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Md5Copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtMd5.Text);
        }

        private void Sha1Copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtSha1.Text);
        }

        private void Sha526Copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtSha256.Text);
        }

        private void Sha512Copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtSha512.Text);
        }
    }
}
