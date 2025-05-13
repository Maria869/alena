using System.Windows;
using System.Windows.Controls;
using Gallery.Services;

namespace Gallery.Views
{
    public partial class RegisterWindow : Window
    {
        private readonly DatabaseService _dbService;

        public RegisterWindow()
        {
            InitializeComponent();
            _dbService = new DatabaseService();
        }

        // Обработчик события для кнопки "Register"
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из текстовых полей
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            // Проверяем, выбрана ли роль
            if (RoleComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.");
                return;
            }
            string role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Регистрируем пользователя через сервис базы данных
            _dbService.RegisterUser(username, password, role);

            // Уведомляем об успешной регистрации
            MessageBox.Show("Registration successful!");

            // Закрываем окно регистрации
            this.Close();
        }
    }
}