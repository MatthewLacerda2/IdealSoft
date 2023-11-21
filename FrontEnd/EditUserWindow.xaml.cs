// EditUserWindow.xaml.cs
using System.Windows;

namespace FrontEnd {
    public partial class EditUserWindow : Window {
        public EditUserWindow() {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Editing saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
