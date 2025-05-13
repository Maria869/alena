using System.Windows;
using Gallery.Views;

namespace Gallery.ViewModels
{
    public class MainViewModel
    {
        // Метод для открытия окна регистрации
        public void ShowRegisterWindow()
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
        }

        // Метод для открытия окна входа
        public void ShowLoginWindow()
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}