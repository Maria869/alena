using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Models
{
    public class User
    {
        // Уникальный идентификатор пользователя
        public int UserID { get; set; }

        // Имя пользователя (логин)
        public string Username { get; set; }

        // Хэшированный пароль (в реальном приложении используйте хэширование!)
        public string PasswordHash { get; set; }

        // Роль пользователя: "Artist" или "Buyer"
        public string Role { get; set; }
    }
}
