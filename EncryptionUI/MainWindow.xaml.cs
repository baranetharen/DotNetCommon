using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EncryptionUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EncryptorModel EncryptorModel;
        public MainWindow()
        {
            InitializeComponent();
            EncryptorModel = new EncryptorModel();
            DataContext = EncryptorModel;
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            EncryptorModel.OutKeyMessage -= new EventHandler<List<string>>(OnKeyGeneration);
            EncryptorModel.OutKeyMessage += new EventHandler<List<string>>(OnKeyGeneration);
            EncryptorModel.OutTextMessage -= new EventHandler<string>(OnTextGeneration);
            EncryptorModel.OutTextMessage += new EventHandler<string>(OnTextGeneration);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var keys = ((ComboBoxItem)NoKeys.SelectedItem).Content;
            EncryptorModel.NoOfKeys = int.TryParse(keys as string, out int key) ? key : 0;
            EncryptorModel.Command = ((ComboBoxItem)command.SelectedItem).Content as string;
            EncryptorModel.Keys = tKeys.Text.Split('\n','\r');
            button.Command.Execute(button.CommandParameter);
            EncryptorModel.Exeute();
        }

        private void OnKeyGeneration(object sender , List<string> keys)
        {
            tKeys.Text =  string.Join("\n",keys.ToArray());
        }

        private void OnTextGeneration(object sender, string keys)
        {
            Text.Text = keys;
        }

        private void OnError(object sender , string Message)
        {
            MessageBox.Show(Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        protected override void OnClosed(EventArgs e)
        {
            UnsubscribeEvents();
            base.OnClosed(e);
        }

        private void UnsubscribeEvents()
        {
            EncryptorModel.OutKeyMessage -= new EventHandler<List<string>>(OnKeyGeneration);
            EncryptorModel.OutTextMessage -= new EventHandler<string>(OnTextGeneration);
        }

        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TKeys_TextChanged(object sender, TextChangedEventArgs e)
        {
        
        }

        private void Command_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
