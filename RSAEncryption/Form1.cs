using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace RSAEncryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(1024))
                {
                    RSA.ImportParameters(RSAKey);
                    encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                }
                return encryptedData;
            }
        }

        static public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(1024))
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                return decryptedData;
            }
        }

        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(1024);
        byte[] plaintext;
        byte[] encryptedtext;
        
        private void folderSelectButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                pathLabel.Text = folderBrowserDialog1.SelectedPath;
            }
        }


        private void SelectFileButton_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileNameLabel.Text = openFileDialog1.FileName;

            }
        }

        private void EncryptButton_Click_1(object sender, EventArgs e)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            plaintext = ByteConverter.GetBytes(TextToEncrypt.Text);
            encryptedtext = Encryption(plaintext, RSA.ExportParameters(false), false);


            string filepath = String.Format(@"{0}\{1}.txt", pathLabel.Text, fileNameTextBox.Text);
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                writer.Write(Convert.ToBase64String(encryptedtext, Base64FormattingOptions.InsertLineBreaks));
            }
        }

        private void DecryptButton_Click_1(object sender, EventArgs e)
        {
            string asd = File.ReadAllText(fileNameLabel.Text);
            byte[] src = Convert.FromBase64String(asd);

            byte[] decryptedtex = Decryption(src, RSA.ExportParameters(true), false);
            DecryptedText.Text = ByteConverter.GetString(decryptedtex);
        }
    }
}