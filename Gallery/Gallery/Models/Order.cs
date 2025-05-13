using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Models
{
    public class Order
    {
        // Уникальный идентификатор заказа
        public int OrderID { get; set; }

        // Идентификатор покупателя, который оформил заказ
        public int BuyerID { get; set; }

        // Адрес доставки
        public string Address { get; set; }

        // Номер карты
        public string CardNumber { get; set; }

        // CVC-код карты
        public string CVC { get; set; }

        // Дата оформления заказа
        public DateTime OrderDate { get; set; }
    }
}
