using DeliveryService.Models;
using System.Linq;

namespace DeliveryService.Patterns.Strategy
{
    public class StandardCostStrategy : ICostCalculationStrategy
    {
        public decimal CalculateTotalCost(Order order)
        {
            decimal itemsTotal = order.Items.Sum(item => item.Price);
            decimal deliveryCost = order.DeliveryType.CalculateDeliveryCost(itemsTotal);
            return itemsTotal + deliveryCost; // Налоги можно добавить здесь при необходимости
        }
    }
}