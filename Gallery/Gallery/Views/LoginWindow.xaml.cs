using System.Windows;
using Gallery.Services;

namespace Gallery.Views
{
    public partial class LoginWindow : Window
    {
        private readonly DatabaseService _dbService;

        public LoginWindow()
        {
            InitializeComponent();
            _dbService = new DatabaseService();
        }

        // Обработчик события для кнопки "Login"
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из текстовых полей
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            // Проверяем учетные данные через сервис базы данных
            var user = _dbService.LoginUser(username, password);

            if (user != null)
            {
                MessageBox.Show($"Welcome, {user.Username}!");

                // Открываем соответствующее окно в зависимости от роли пользователя
                if (user.Role == "Artist")
                {
                    // Например, открываем окно художника
                    //var artistWindow = new ArtistWindow(); // Создайте это окно позже
                    //artistWindow.Show();
                }
                else if (user.Role == "Buyer")
                {
                    // Например, открываем окно покупателя
                    var galleryView = new GalleryView();
                    galleryView.Show();
                }

                // Закрываем окно входа
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
    }
}