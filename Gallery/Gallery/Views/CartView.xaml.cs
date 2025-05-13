using Npgsql;
using System;
using System.Collections.Generic;
using Gallery.Models;

namespace ArtGalleryApp.Services
{
    public class DatabaseService
    {
        // Строка подключения к базе данных PostgreSQL
        private string connectionString = "Host=localhost;Port=5432;Database=gallery;Username=postgres;Password=vfhbz2006";

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        public void RegisterUser(string username, string password, string role)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new NpgsqlCommand(@"
                    INSERT INTO Users (Username, PasswordHash, Role) 
                    VALUES (@username, @password, @role)", conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password); // Для реального приложения используйте хэширование!
                cmd.Parameters.AddWithValue("role", role);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Проверка учетных данных пользователя
        /// </summary>
        public User LoginUser(string username, string password)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new NpgsqlCommand(@"
                    SELECT * FROM Users 
                    WHERE Username = @username AND PasswordHash = @password", conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            UserID = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            PasswordHash = reader.GetString(2),
                            Role = reader.GetString(3)
                        };
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Получение списка доступных картин
        /// </summary>
        public List<Artwork> GetArtworks()
        {
            var artworks = new List<Artwork>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new NpgsqlCommand(@"
                    SELECT * FROM Artworks 
                    WHERE IsSold = FALSE", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        artworks.Add(new Artwork
                        {
                            ArtworkID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            Price = reader.GetDecimal(3),
                            ArtistID = reader.GetInt32(4),
                            IsSold = reader.GetBoolean(5)
                        });
                    }
                }
            }
            return artworks;
        }

        /// <summary>
        /// Добавление картины в корзину
        /// </summary>
        public void AddToCart(int buyerID, int artworkID)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new NpgsqlCommand(@"
                    INSERT INTO Cart (BuyerID, ArtworkID) 
                    VALUES (@buyerID, @artworkID)", conn);
                cmd.Parameters.AddWithValue("buyerID", buyerID);
                cmd.Parameters.AddWithValue("artworkID", artworkID);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Получение списка картин в корзине
        /// </summary>
        public List<Artwork> GetCartItems(int buyerId)
        {
            var artworks = new List<Artwork>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new NpgsqlCommand(@"
                    SELECT a.* 
                    FROM Artworks a
                    INNER JOIN Cart c ON a.ArtworkID = c.ArtworkID
                    WHERE c.BuyerID = @buyerId", conn);
                cmd.Parameters.AddWithValue("buyerId", buyerId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        artworks.Add(new Artwork
                        {
                            ArtworkID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            Price = reader.GetDecimal(3),
                            ArtistID = reader.GetInt32(4),
                            IsSold = reader.GetBoolean(5)
                        });
                    }
                }
            }
            return artworks;
        }

        /// <summary>
        /// Оформление заказа
        /// </summary>
        public void PlaceOrder(Order order)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                // Добавляем заказ в таблицу Orders
                var cmd = new NpgsqlCommand(@"
                    INSERT INTO Orders (BuyerID, Address, CardNumber, CVC, OrderDate)
                    VALUES (@buyerId, @address, @cardNumber, @cvc, @orderDate)", conn);
                cmd.Parameters.AddWithValue("buyerId", order.BuyerID);
                cmd.Parameters.AddWithValue("address", order.Address);
                cmd.Parameters.AddWithValue("cardNumber", order.CardNumber);
                cmd.Parameters.AddWithValue("cvc", order.CVC);
                cmd.Parameters.AddWithValue("orderDate", order.OrderDate);
                cmd.ExecuteNonQuery();

                // Помечаем картины как проданные
                var updateCmd = new NpgsqlCommand(@"
                    UPDATE Artworks 
                    SET IsSold = TRUE 
                    WHERE ArtworkID IN (SELECT ArtworkID FROM Cart WHERE BuyerID = @buyerId)", conn);
                updateCmd.Parameters.AddWithValue("buyerId", order.BuyerID);
                updateCmd.ExecuteNonQuery();

                // Очищаем корзину
                var clearCartCmd = new NpgsqlCommand(@"
                    DELETE FROM Cart 
                    WHERE BuyerID = @buyerId", conn);
                clearCartCmd.Parameters.AddWithValue("buyerId", order.BuyerID);
                clearCartCmd.ExecuteNonQuery();
            }
        }
    }
}