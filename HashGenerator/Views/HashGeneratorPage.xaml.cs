﻿using Extensions;
using Extensions.Encryption;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace HashGenerator.Views
{
    /// <summary>
    /// Interaction logic for HashGeneratorPage.xaml
    /// </summary>
    public partial class HashGeneratorPage : Page, INotifyPropertyChanged
    {
        private string _argon2Key, _pbkdf2Key, md5Hash;

        #region Modifying Properties

        /// <summary>Represents the hashed Argon2 key.</summary>
        public string Argon2Key
        {
            get => _argon2Key;
            set
            {
                _argon2Key = value;
                OnPropertyChanged("Argon2Key");
            }
        }

        /// <summary>Represents the hashed PBKDF2 key.</summary>
        public string PBKDF2Key
        {
            get => _pbkdf2Key;
            set
            {
                _pbkdf2Key = value;
                OnPropertyChanged("PBKDF2Key");
            }
        }

        /// <summary>Represents the MD5 hash.</summary>
        public string Md5Hash
        {
            get => md5Hash;
            set
            {
                md5Hash = value;
                OnPropertyChanged("Md5Hash");
            }
        }

        #endregion Modifying Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Allows data bound controls to acknowledge that a change to a Property has occurred.</summary>
        /// <param name="property">Name of Property</param>
        private void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Argon2

        /// <summary>Converts the plaintext to an Argon2 key.</summary>
        private void ConvertArgon2() => Argon2Key = Argon2.HashPassword(TxtArgon2Plaintext.Text);

        /// <summary>Clears both Argon2 TextBoxes and sets the focus to the plaintext TextBox.</summary>
        private void ClearArgon2()
        {
            TxtArgon2Plaintext.Clear();
            Argon2Key = "";
            TxtArgon2Plaintext.Focus();
        }

        /// <summary>Copies the Argon2 Key to the clipboard.</summary>
        private void CopyArgon2ToClipboard()
        {
            Clipboard.SetText(TxtArgon2Key.Text);
            TxtArgon2Plaintext.Focus();
        }

        private void TxtArgon2Plaintext_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtArgon2Plaintext.Text.Length > 0)
                ConvertArgon2();
            else
                ClearArgon2();

            CheckButtons();
        }

        #endregion Argon2

        #region PBKDF2

        /// <summary>Converts the plaintext to a PBKDF2 key.</summary>
        private void ConvertPBKDF2() => PBKDF2Key = PBKDF2.HashPassword(TxtPBKDF2Plaintext.Text);

        /// <summary>Clears both PBKDF2 TextBoxes and sets the focus to the plaintext TextBox.</summary>
        private void ClearPBKDF2()
        {
            TxtPBKDF2Plaintext.Clear();
            PBKDF2Key = "";
            TxtPBKDF2Plaintext.Focus();
        }

        /// <summary>Copies the PBKDF2 Key to the clipboard.</summary>
        private void CopyPBKDF2ToClipboard()
        {
            Clipboard.SetText(TxtPBKDF2Key.Text);
            TxtPBKDF2Plaintext.Focus();
        }

        private void TxtPBKDF2Plaintext_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtPBKDF2Plaintext.Text.Length > 0)
                ConvertPBKDF2();
            else
                ClearPBKDF2();

            CheckButtons();
        }

        #endregion PBKDF2

        #region MD5

        /// <summary>Converts the plaintext to an MD5 key.</summary>
        private void ConvertMD5() => Md5Hash = MD5.HashMD5(TxtMD5Plaintext.Text);

        /// <summary>Clears both MD5 TextBoxes and sets the focus to the plaintext TextBox.</summary>
        private void ClearMD5()
        {
            TxtMD5Plaintext.Clear();
            Md5Hash = "";
            TxtMD5Plaintext.Focus();
        }

        /// <summary>Copies the MD5 Key to the clipboard.</summary>
        private void CopyMD5ToClipboard()
        {
            Clipboard.SetText(TxtMD5Key.Text);
            TxtMD5Plaintext.Focus();
        }

        private void TxtMD5Plaintext_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtMD5Plaintext.Text.Length > 0)
                ConvertMD5();
            else
                ClearMD5();

            CheckButtons();
        }

        #endregion MD5

        #region Button-Click Methods

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            if (TabArgon2.IsSelected)
                ClearArgon2();
            else if (TabPBKDF2.IsSelected)
                ClearPBKDF2();
            else if (TabMD5.IsSelected)
                ClearMD5();
        }

        private void BtnClipboard_Click(object sender, RoutedEventArgs e)
        {
            if (TabArgon2.IsSelected)
                CopyArgon2ToClipboard();
            else if (TabPBKDF2.IsSelected)
                CopyPBKDF2ToClipboard();
            else if (TabMD5.IsSelected)
                CopyMD5ToClipboard();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        #endregion Button-Click Methods

        #region Button-Manipulation

        /// <summary>Checks whether the Clear and Copy Buttons should be enabled.</summary>
        private void CheckButtons() => BtnClear.IsEnabled = BtnClipboard.IsEnabled = TabArgon2.IsSelected ? TxtArgon2Plaintext.Text.Length > 0 : TabPBKDF2.IsSelected ? TxtPBKDF2Plaintext.Text.Length > 0 : TabMD5.IsSelected && TxtMD5Plaintext.Text.Length > 0;

        #endregion Button-Manipulation

        #region Window-Manipulation Methods

        public HashGeneratorPage() => DataContext = this;

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButtons();

        private void Page_Loaded(object sender, RoutedEventArgs e) =>
            TxtArgon2Plaintext.Focus();

        private void TxtPlaintext_GotFocus(object sender, RoutedEventArgs e) => Functions.TextBoxGotFocus(sender);

        #endregion Window-Manipulation Methods
    }
}