using BSoftEncryptor.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BSoftEncryptor.Views
{
    /// <summary>
    /// Interaction logic for UcGenerateKeys.xaml
    /// </summary>
    public partial class UcGenerateKeys : UserControl
    {
        public UcGenerateKeys()
        {
            InitializeComponent();
        }
        GenerateKeys generateKeys = new GenerateKeys();
        private void BtnGenerateKey_Click(object sender, RoutedEventArgs e)
        {
            generateKeys.AssignNewKey();
            MessageBox.Show("GENERATE KEY SUCCESS", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            txtPrivateKey.Text = generateKeys.privateKey;
            txtPublicKey.Text = generateKeys.publicKey;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var saveFoder = new System.Windows.Forms.FolderBrowserDialog();
            saveFoder.ShowDialog();
            string pathKey = saveFoder.SelectedPath;
            generateKeys.SaveKeys(pathKey, txtPublicKey.Text, txtPrivateKey.Text);
            MessageBox.Show("SAVE KEY SUCCESS", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Asterisk);

        }
    }
}
