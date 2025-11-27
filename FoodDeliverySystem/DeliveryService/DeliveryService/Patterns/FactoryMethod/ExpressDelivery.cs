namespace DeliveryService.Patterns.FactoryMethod
{
    public class ExpressDelivery : DeliveryService
    {
        public override decimal CalculateDeliveryCost(decimal orderTotal)
        {
            // Стоимость экспресс-доставки - 20% от суммы заказа
            return orderTotal * 0.2m;
        }
        
        public override string GetDeliveryType()
        {
            return "Экспресс-доставка";
        }
    }
}