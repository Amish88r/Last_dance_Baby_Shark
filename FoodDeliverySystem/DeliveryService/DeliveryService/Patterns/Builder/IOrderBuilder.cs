using DeliveryService.Models;

namespace DeliveryService.Patterns.Builder
{
    public interface IOrderBuilder
    {
        void AddMenuItem(MenuItem item);
        void SetDeliveryAddress(string address);
        void SetDeliveryType(FactoryMethod.DeliveryService deliveryType);
        Order GetOrder();
    }
}