using DeliveryService.Models;
using System.Linq;

namespace DeliveryService.Patterns.Strategy
{
    public class DiscountCostStrategy : ICostCalculationStrategy
    {
        private readonly decimal _discountPercentage;

        public DiscountCostStrategy(decimal discountPercentage)
        {
            _discountPercentage = discountPercentage;
        }

        public decimal CalculateTotalCost(Order order)
        {
            decimal itemsTotal = order.Items.Sum(item => item.Price);
            decimal discountedItemsTotal = itemsTotal * (1 - _discountPercentage / 100);
            decimal deliveryCost = order.DeliveryType.CalculateDeliveryCost(discountedItemsTotal);
            return discountedItemsTotal + deliveryCost;
        }
    }
}