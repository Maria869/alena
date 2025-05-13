using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Models
{
    public class Artwork
    {
        // Уникальный идентификатор картины
        public int ArtworkID { get; set; }

        // Название картины
        public string Title { get; set; }

        // Описание картины
        public string Description { get; set; }

        // Цена картины
        public decimal Price { get; set; }

        // Идентификатор художника, который загрузил картину
        public int ArtistID { get; set; }

        // Флаг, указывающий, продана ли картина
        public bool IsSold { get; set; }
    }
}
