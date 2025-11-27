using DeliveryService.Models;

namespace DeliveryService.Patterns.Observer
{
    public interface IOrderObserver
    {
        void Update(Order order);
    }
}