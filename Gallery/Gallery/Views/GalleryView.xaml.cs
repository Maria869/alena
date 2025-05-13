using System.Collections.Generic;
using System.Windows;
using Gallery.Models;
using Gallery.Services;

namespace Gallery.Views
{
    public partial class GalleryView : Window
    {
        private readonly DatabaseService _dbService;
        private int _currentUserId; // ID текущего пользователя

        public GalleryView(int userId)
        {
            InitializeComponent();
            _dbService = new DatabaseService();
            _currentUserId = userId;

            // Загружаем список картин
            LoadArtworks();
        }

        // Метод для загрузки картин из базы данных
        private void LoadArtworks()
        {
            var artworks = _dbService.GetArtworks();
            DataContext = new { Artworks = artworks };
        }

        // Обработчик события для кнопки "Go to Cart"
        private void GoToCart_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно корзины
            var cartView = new CartView(_currentUserId);
            cartView.Show();

            // Закрываем текущее окно
            this.Close();
        }
    }
}