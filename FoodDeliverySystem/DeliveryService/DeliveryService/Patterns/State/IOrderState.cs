using DeliveryService.Models;

namespace DeliveryService.Patterns.State
{
    public interface IOrderState
    {
        void NextState(Order order);
        void PreviousState(Order order);
        string GetStatus(Order order);
    }
}