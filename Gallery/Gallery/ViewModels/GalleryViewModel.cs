using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Gallery.Models;
using Gallery.Services;

namespace Gallery.ViewModels
{
    public class GalleryViewModel
    {
        private readonly DatabaseService _dbService;
        private int _currentUserId;

        // Список картин
        public List<Artwork> Artworks { get; set; }

        // Команда для добавления картины в корзину
        public ICommand AddToCartCommand { get; }

        public GalleryViewModel(int userId)
        {
            _dbService = new DatabaseService();
            _currentUserId = userId;

            // Загружаем список картин
            LoadArtworks();

            // Инициализируем команду
            AddToCartCommand = new RelayCommand(AddToCart);
        }

        // Метод для загрузки картин
        private void LoadArtworks()
        {
            Artworks = _dbService.GetArtworks();
        }

        // Метод для добавления картины в корзину
        private void AddToCart(object artworkId)
        {
            if (artworkId is int id)
            {
                _dbService.AddToCart(_currentUserId, id);
                MessageBox.Show("Artwork added to cart!");
            }
        }
    }
}