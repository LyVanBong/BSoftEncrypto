﻿using BSoftEncryptor.ViewModels;
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

            }
        }
        private void RdEncryptor_Checked(object sender, RoutedEventArgs e)
        {
            if (checks > 0)
            {
                publicKeys.IsEnabled = true;
                keys.IsEnabled = false;
                txtKeys.Clear();
                checks++;
            }
        }

        private void RdDecryptor_Checked(object sender, RoutedEventArgs e)
        {
            keys.IsEnabled = true;
            publicKeys.IsEnabled = false;
            txtPublicKey.Clear();
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
                    string a = txtPublicKey.Text;
                    string b = txtFileOpen.Text;
                    rsa = new AsymmetricAlgorithms(b);
                    rsa.EncryptData(a);
                    MessageBox.Show("ENCRYPTOR SUCCESS", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ENCRYPTOR FAIL: " + ex.Message, "FAIL", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("DECRYPTOR SUCCESS", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("DECRYPTOR FAIL: " + ex.Message, "FAIL", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnPubKey_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtPublicKey.Text = openFileDialog.FileName;
            }
        }
    }
}
