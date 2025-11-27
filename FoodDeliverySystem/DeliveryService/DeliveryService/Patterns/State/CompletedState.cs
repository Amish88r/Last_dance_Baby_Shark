// Файл: CompletedState.cs
using DeliveryService.Models;
using System;

namespace DeliveryService.Patterns.State
{
    public class CompletedState : IOrderState
    {
        public void NextState(Order order)
        {
            Console.WriteLine("Заказ уже выполнен и находится в конечном состоянии.");
        }

        public void PreviousState(Order order)
        {
            order.SetState(new DeliveringState());
        }

        public string GetStatus(Order order)
        {
            return "Заказ выполнен";
        }
    }
}