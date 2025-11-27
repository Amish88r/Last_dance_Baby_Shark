// Файл: PreparingState.cs
using DeliveryService.Models;
using System;

namespace DeliveryService.Patterns.State
{
    public class PreparingState : IOrderState
    {
        public void NextState(Order order)
        {
            order.SetState(new DeliveringState());
        }

        public void PreviousState(Order order)
        {
            Console.WriteLine("Заказ уже находится в начальном состоянии.");
        }

        public string GetStatus(Order order)
        {
            return "Заказ готовится";
        }
    }
}