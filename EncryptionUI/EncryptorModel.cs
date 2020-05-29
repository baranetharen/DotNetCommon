using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using AESEncryptor;
namespace EncryptionUI
{
    public class EncryptorModel
    {
        public ICommand passwordUpdater { get; set; }
        public int NoOfKeys { get; set; }
        public string Command;
        public IList<string> Keys;
        private string _text;
        public EventHandler<List<string>> OutKeyMessage = null;
        public EventHandler<string> OutTextMessage = null;
        public EventHandler<string> Error = null;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }
        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public EncryptorModel()
        {
            passwordUpdater = new PasswordUpdater(SetPassword);
        }

        private void SetPassword(object passsword)
        {
            string passwordval;
            if ((passwordval = passsword as string) != string.Empty)
            {
                Password = passwordval;
            }
        }

        private bool Validate()
        {
            return NoOfKeys != 0 && !string.IsNullOrEmpty(Text) && !string.IsNullOrWhiteSpace(Text) &&
                  !string.IsNullOrEmpty(Password) && !string.IsNullOrWhiteSpace(Password) &&
                  !string.IsNullOrEmpty(Command) && !string.IsNullOrWhiteSpace(Command);
        }

        public void Exeute()
        {

            try
            {
                AESEncryptor.CommandExecutor commandExecutor = new CommandExecutor();
                var cmd = Command.Replace(" ", "").ToLower();
                if (cmd.StartsWith("to") && Validate())
                {
                    commandExecutor.GenerateKeys(Text, Password, out IEnumerable<SecurityKey> genkeys);
                    if (genkeys == null)
                    {
                        Error?.Invoke(this, "Error in GenerateKeys");
                    }
                    OutKeyMessage?.Invoke(this, genkeys.Select(x => x.Key).ToList());
                }
                else if (cmd.StartsWith("from"))
                {
                    if (Keys != null)
                    {
                        if (Keys.Count == 0)
                        {
                            Error?.Invoke(this, "Please Enter The Keys");
                            return;
                        }
                        commandExecutor.GenerateText(Keys, Password, out string text);
                        if (text == null) { Error?.Invoke(this, "Please Enter The Keys"); return; }
                        OutTextMessage?.Invoke(this, text);
                    }
                }
                else
                {
                    Error?.Invoke(this, "Invalid Command"); return;
                }
            }
            catch (Exception ex)
            {
                Error?.Invoke(this, ex.Message);
            }
        }

        internal class PasswordUpdater : ICommand
        {
            private readonly Action<object> passwordaction = null;
            public PasswordUpdater(Action<object> action)
            {
                this.passwordaction = action;
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
            public bool CanExecute(object parameter)
            {
                if (parameter is PasswordBox)
                {

                }
                return true;
            }

            public void Execute(object parameter)
            {
                PasswordBox passwordBox = null;
                if (parameter is PasswordBox)
                {
                    passwordBox = parameter as PasswordBox;
                }
                string password = passwordBox.Password;
                passwordaction(password);
            }
        }
    }
}