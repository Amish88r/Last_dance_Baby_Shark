using DeliveryService.Models;

namespace DeliveryService.Patterns.Strategy
{
    public interface ICostCalculationStrategy
    {
        decimal CalculateTotalCost(Order order);
    }
}