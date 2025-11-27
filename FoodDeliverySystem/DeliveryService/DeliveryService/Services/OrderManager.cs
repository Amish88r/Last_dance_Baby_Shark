using DeliveryService.Models;
using DeliveryService.Patterns.Builder;
using DeliveryService.Patterns.Strategy;

namespace DeliveryService.Services
{
    public class OrderManager
    {
        private readonly IOrderBuilder _orderBuilder;
        private ICostCalculationStrategy _costCalculationStrategy;

        public OrderManager(IOrderBuilder orderBuilder, ICostCalculationStrategy costCalculationStrategy)
        {
            _orderBuilder = orderBuilder;
            _costCalculationStrategy = costCalculationStrategy;
        }

        public void SetCostCalculationStrategy(ICostCalculationStrategy costCalculationStrategy)
        {
            _costCalculationStrategy = costCalculationStrategy;
        }

        public Order CreateOrder(MenuItem[] items, string address, Patterns.FactoryMethod.DeliveryService deliveryType)
        {
            foreach (var item in items)
            {
                _orderBuilder.AddMenuItem(item);
            }
            _orderBuilder.SetDeliveryAddress(address);
            _orderBuilder.SetDeliveryType(deliveryType);

            Order order = _orderBuilder.GetOrder();
            order.TotalCost = _costCalculationStrategy.CalculateTotalCost(order);
            return order;
        }
    }
}