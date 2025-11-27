namespace DeliveryService.Patterns.FactoryMethod
{
    public class StandardDelivery : DeliveryService
    {
        public override decimal CalculateDeliveryCost(decimal orderTotal)
        {
            // Стоимость стандартной доставки - 10% от суммы заказа
            return orderTotal * 0.1m;
        }

        public override string GetDeliveryType()
        {
            return "Стандартная доставка";
        }
    }
}