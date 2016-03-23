using System;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.Serialization;
namespace SimpleCipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SimpleCipherManager.SetCharacters();
        }

        private void cryptoInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            SimpleCipherManager.inputString = cryptoInput.Text;
        }

        private void generateCryptoKeyButton_Click(object sender, RoutedEventArgs e)
        {
            SimpleCipherManager.SetCipherKey(SimpleCipherManager.CreateCipherKey());
            keyStatusText.Text = "Key Set";
            cryptoOutput.Text = "A new Cipher Key has been been created. You can write a new message to encode in the text box above.\n"+
                "This key will be gone once you close the program unless you choose to save it. Simple Cipher cannot decipher a message unless the correct cryptoKey is used.";
        }

        private void encodeButton_Click(object sender, RoutedEventArgs e)
        {
            cryptoOutput.Text = SimpleCipherManager.EncodeMessage(cryptoInput.Text);
        }

        private void decodeButton_Click(object sender, RoutedEventArgs e)
        {
            cryptoOutput.Text = SimpleCipherManager.DecodeMessage(cryptoInput.Text);
        }

        private void saveCryptoKeyButton_Click(object sender, RoutedEventArgs e)
        {
            if (SimpleCipherManager.cipherKey != null)
            {
                SaveKey();
                cryptoOutput.Text = "Cipher Key has been saved as 'cryptoKey.ckey' in the same directory as Simple Cipher.";
            }
            else
            {
                cryptoOutput.Text = "No Cipher Key found. Have you generated a new Cipher Key yet?";
            }
        }

        public void SaveKey()
        {
            if (SimpleCipherManager.cipherKey != null)
            {
                IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.Stream stream = new System.IO.FileStream(AppDomain.CurrentDomain.BaseDirectory + "/cryptoKey.ckey", System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);
                formatter.Serialize(stream, SimpleCipherManager.cipherKey);
                stream.Close();
            }
        }

        public void LoadKey()
        {
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/cryptoKey.ckey"))
            {
                IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.Stream stream = new System.IO.FileStream(AppDomain.CurrentDomain.BaseDirectory + "/cryptoKey.ckey", System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                CipherKey cryptoKey = (CipherKey)formatter.Deserialize(stream);
                stream.Close();
                SimpleCipherManager.cipherKey = cryptoKey;
            }
        }

        private void loadCryptoKeyButton_Click(object sender, RoutedEventArgs e)
        {
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/cryptoKey.ckey"))
            {
                LoadKey();
                keyStatusText.Text = "Key Set";
            }
            else
            {
                cryptoOutput.Text = "No Cipher Key (cryptoKey.ckey) file was found in the same directory as Simple Cipher. Have you Created and saved a key yet?";
            }
        }

        private void clipboardButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(cryptoOutput.Text);
        }
    }
}
