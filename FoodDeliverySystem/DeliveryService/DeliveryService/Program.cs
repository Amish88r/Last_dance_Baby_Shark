using System;
using DeliveryService.Models;
using DeliveryService.Patterns.Builder;
using DeliveryService.Patterns.FactoryMethod;
using DeliveryService.Patterns.Observer;
using DeliveryService.Patterns.Strategy;
using DeliveryService.Services;

namespace DeliveryService.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Для корректного отображения кириллицы в консоли
            
            // --- Инициализация основных компонентов ---
            var builder = new OrderBuilder();
            var standardCostStrategy = new StandardCostStrategy();
            
            // OrderManager изначально создается со стандартной стратегией расчета
            var orderManager = new OrderManager(builder, standardCostStrategy);
            
            // --- Демонстрация 1: Стандартный заказ ---
            Console.WriteLine("--- Создание стандартного заказа ---");
            
            // Создаем меню для заказа
            var standardOrderItems = new MenuItem[] 
            {
                new MenuItem("Пицца 'Четыре сыра'", 650.50m),
                new MenuItem("Лимонад", 120.00m)
            };
            
            // Используем фабричный метод для создания типа доставки
            var standardDelivery = new StandardDelivery();
            
            // Создаем заказ через OrderManager
            Order order1 = orderManager.CreateOrder(standardOrderItems, "г. Москва, ул. Ленина, д. 15, кв. 45", standardDelivery);

            // Выводим информацию о заказе
            Console.WriteLine($"Создан заказ. Адрес доставки: {order1.DeliveryAddress}");
            Console.WriteLine($"Тип доставки: {order1.DeliveryType.GetDeliveryType()}");
            Console.WriteLine($"Итоговая стоимость: {order1.TotalCost:C}"); // :C форматирует как валюту
            Console.WriteLine($"Текущий статус: {order1.GetStatus()}");
            
            // Демонстрация работы паттерна "Наблюдатель"
            Console.WriteLine("\n--- Демонстрация отслеживания статуса заказа ---");
            var smsNotifier = new SmsNotifier();
            order1.AddObserver(smsNotifier);
            
            Console.WriteLine("Переводим заказ в следующее состояние...");
            order1.NextState(); // Из "Готовится" в "Доставляется". SmsNotifier должен сработать.
            
            Console.WriteLine("\nПереводим заказ в следующее состояние...");
            order1.NextState(); // Из "Доставляется" в "Выполнен". SmsNotifier должен сработать.
            Console.WriteLine($"Финальный статус: {order1.GetStatus()}");
            
            Console.WriteLine("\n============================================\n");

            // --- Демонстрация 2: Заказ с экспресс-доставкой и скидкой ---
            Console.WriteLine("--- Создание заказа с экспресс-доставкой и скидкой ---");
            
            // Демонстрация работы паттерна "Стратегия": меняем стратегию расчета стоимости
            var discountStrategy = new DiscountCostStrategy(10); // 10% скидка
            orderManager.SetCostCalculationStrategy(discountStrategy);
            
            var expressOrderItems = new MenuItem[]
            {
                new MenuItem("Суши-сет 'Дракон'", 1800.00m),
                new MenuItem("Мисо-суп", 250.00m)
            };
            
            // Используем фабричный метод для другого типа доставки
            var expressDelivery = new ExpressDelivery();
            
            Order order2 = orderManager.CreateOrder(expressOrderItems, "г. Санкт-Петербург, Невский пр., д. 1", expressDelivery);

            Console.WriteLine($"Создан заказ. Адрес доставки: {order2.DeliveryAddress}");
            Console.WriteLine($"Тип доставки: {order2.DeliveryType.GetDeliveryType()}");
            Console.WriteLine($"Итоговая стоимость (со скидкой 10% и экспресс-доставкой): {order2.TotalCost:C}");
            Console.WriteLine($"Текущий статус: {order2.GetStatus()}");
            
            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}