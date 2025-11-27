using DeliveryService.Models;

namespace DeliveryService.Patterns.Builder
{
    public class OrderBuilder : IOrderBuilder
    {
        private Order _order = new Order();

        public void AddMenuItem(MenuItem item)
        {
            _order.Items.Add(item);
        }

        public void SetDeliveryAddress(string address)
        {
            _order.DeliveryAddress = address;
        }

        public void SetDeliveryType(FactoryMethod.DeliveryService deliveryType)
        {
            _order.DeliveryType = deliveryType;
        }

        public Order GetOrder()
        {
            Order result = _order;
            // Сбрасываем строитель для следующего заказа
            _order = new Order();
            return result;
        }
    }
}