using DeliveryService.Models;
using System;

namespace DeliveryService.Patterns.Observer
{
    public class SmsNotifier : IOrderObserver
    {
        public void Update(Order order)
        {
            // В реальном приложении здесь был бы код для отправки СМС
            Console.WriteLine($"СМС-Уведомление: Статус вашего заказа изменен на: '{order.GetStatus()}'.");
        }
    }
}