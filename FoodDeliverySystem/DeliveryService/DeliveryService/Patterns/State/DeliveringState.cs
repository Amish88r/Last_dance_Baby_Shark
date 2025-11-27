// Файл: DeliveringState.cs
using DeliveryService.Models;

namespace DeliveryService.Patterns.State
{
    public class DeliveringState : IOrderState
    {
        public void NextState(Order order)
        {
            order.SetState(new CompletedState());
        }

        public void PreviousState(Order order)
        {
            order.SetState(new PreparingState());
        }

        public string GetStatus(Order order)
        {
            return "Заказ доставляется";
        }
    }
}