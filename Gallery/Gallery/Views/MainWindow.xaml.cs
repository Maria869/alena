using System.Windows;
using Gallery.Views;

namespace Gallery.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик события для кнопки "Register"
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно регистрации
            var registerWindow = new RegisterWindow();
            registerWindow.Show();

            // Закрываем главное окно (опционально)
            this.Close();
        }

        // Обработчик события для кнопки "Login"
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно входа
            var loginWindow = new LoginWindow();
            loginWindow.Show();

            // Закрываем главное окно (опционально)
            this.Close();
        }
    }
}