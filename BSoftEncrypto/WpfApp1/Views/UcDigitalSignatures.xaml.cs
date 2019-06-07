using BSoftEncryptor.ViewModels;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace BSoftEncryptor.Views
{
    /// <summary>
    /// Interaction logic for UcDigitalSignatures.xaml
    /// </summary>
    public partial class UcDigitalSignatures : UserControl
    {
        private bool isChecks = false;
        private byte[] signData;
        private DigitalSignatures signatures = new DigitalSignatures();

        public UcDigitalSignatures()
        {
            InitializeComponent();
        }

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtFileOpen.Text = openFileDialog.FileName;
            }
        }


        private void BtnPubOrPriKey_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtPubPriKey.Text = openFileDialog.FileName;
            }
        }

        private void BtnGign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                signData= signatures.SignByRSA(txtPubPriKey.Text,txtFileOpen.Text);
                MessageBox.Show("SIGN DIGITAL SUCCESS", "SIGN", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("SIGN DIGITAL SUCCESS: " + ex, "FAIL", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RdSign_Checked(object sender, RoutedEventArgs e)
        {
            if (isChecks != false)
            {
                txtPubPriKey.Clear();
                txtFileCer.Clear();
                lbPuborPriKey.Content = "PRIVATE KEY";
                btnVerify.Visibility = Visibility.Hidden;
                btnGign.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;
                fileCer.IsEnabled = false;
            }
        }

        private void RdVerify_Checked(object sender, RoutedEventArgs e)
        {
            txtPubPriKey.Clear();
            txtFileCer.Clear();
            lbPuborPriKey.Content = "PUBLIC KEY";
            btnVerify.Visibility = Visibility.Visible;
            btnGign.Visibility = Visibility.Hidden;
            fileCer.IsEnabled = true;
            btnSave.Visibility = Visibility.Hidden;
            isChecks = true;
        }

        private void BtnChooseFileCer_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtFileCer.Text = openFileDialog.FileName;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var saveFoder = new System.Windows.Forms.FolderBrowserDialog();
            saveFoder.ShowDialog();
            string pathKey = saveFoder.SelectedPath;

            signatures.SaveSign(pathKey,signData,txtFileOpen.Text);
            MessageBox.Show("SAVE SIGN SUCCESS", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void BtnVerify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (signatures.VerifyByRSA(txtFileOpen.Text,txtPubPriKey.Text,txtFileCer.Text) == true)
                {
                    MessageBox.Show("SIGNATURE IS VALID", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    MessageBox.Show("SIGNATURE IS NOT VALID", "FAIL", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("VERIFY FAIL: " + ex, "FAIL", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
