namespace DeliveryService.Patterns.FactoryMethod
{
    // Абстрактный класс для фабричного метода
    public abstract class DeliveryService
    {
        public abstract decimal CalculateDeliveryCost(decimal orderTotal);
        public abstract string GetDeliveryType();
    }
}