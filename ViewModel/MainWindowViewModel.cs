using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using RSA_App.Model;

namespace RSA_App.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        // Class instance declaration
        private readonly MyRSA _MyRSA = new ();

        #region PublicKey

        private string? _publicKey;

        public string? PublicKey
        {
            get => _publicKey;
            set => SetProperty(ref _publicKey, value);
        }

        #endregion

        #region PrivateKey

        private string? _privateKey;

        public string? PrivateKey
        {
            get => _privateKey;
            set => SetProperty(ref _privateKey, value);
        }

        #endregion

        #region KeysLength

        public ObservableCollection<int> KeysLength { get; } = new() { 384, 512, 1024, 2048, 4096};

        private int _selectedKeyLength;

        public int SelectedKeyLength
        {
            get => _selectedKeyLength;
            set => SetProperty(ref _selectedKeyLength, value);
        }

        #endregion

        #region PlainText

        private string? _plainText;

        public string? PlainText
        {
            get => _plainText;
            set => SetProperty(ref _plainText, value);
        }

        #endregion

        #region CipherText

        private string? _cipherText;

        public string? CipherText
        {
            get => _cipherText;
            set => SetProperty(ref _cipherText, value);
        }

        #endregion

        #region CryptedMessage

        private string? _cryptedMessage;

        public string? CryptedMessage
        {
            get => _cryptedMessage;
            set => SetProperty(ref _cryptedMessage, value);
        }

        #endregion

        #region DecryptedMessage

        private string? _decryptedMessage;

        public string? DecryptedMessage
        {
            get => _decryptedMessage;
            set => SetProperty(ref _decryptedMessage, value);
        }

        #endregion

        #region EncryptionCommand

        public ICommand EncryptionCommand => new DelegateCommand(() =>
        {
            if (!string.IsNullOrEmpty(PlainText) && !string.IsNullOrEmpty(PublicKey))
                CryptedMessage = _MyRSA.Encrypt(PlainText, PublicKey);
            else
                MessageBox.Show("Поле публичного ключа или текста не могут быть пустыми");
        });

        #endregion

        #region DecryptionCommand

        public ICommand DecryptionCommand => new DelegateCommand(() =>
        {

            if (!string.IsNullOrEmpty(CipherText) && !string.IsNullOrEmpty(PrivateKey))
                DecryptedMessage = _MyRSA.Decrypt(CipherText, PrivateKey);
            else
                MessageBox.Show("Поле приватного ключа или текста не могут быть пустыми");
        });

        #endregion

        #region GenerateKeyCommand

        public ICommand GenerateKeyCommand => new DelegateCommand(() =>
        {
            _MyRSA.KeyValue = SelectedKeyLength;

            if (_MyRSA.KeyValue is 0)
                MessageBox.Show("Выберите длину ключа");
            else
            {
                var keyPair = _MyRSA.GenerateKeyPair();
                PrivateKey = keyPair.Item1;
                PublicKey = keyPair.Item2;
            }
        });

        #endregion
    }
}
