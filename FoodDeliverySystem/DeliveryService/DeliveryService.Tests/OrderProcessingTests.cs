using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeliveryService.Models;
using DeliveryService.Patterns.Builder;
using DeliveryService.Patterns.FactoryMethod;
using DeliveryService.Patterns.State;
using DeliveryService.Patterns.Strategy;
using DeliveryService.Patterns.Observer;
using DeliveryService.Services;
using System.IO;
using System;

namespace DeliveryService.Tests
{
    [TestClass]
    public class OrderProcessingTests
    {
        [TestMethod]
        public void CreateOrder_WithStandardDelivery_CalculatesCorrectTotalCost()
        {
            // Arrange
            var builder = new OrderBuilder();
            var standardStrategy = new StandardCostStrategy();
            var orderManager = new OrderManager(builder, standardStrategy);
            var items = new MenuItem[] { new MenuItem("Пицца 'Маргарита'", 500), new MenuItem("Кола", 100) };
            var deliveryService = new StandardDelivery();
            decimal expectedItemsCost = 600;
            decimal expectedDeliveryCost = expectedItemsCost * 0.1m;
            decimal expectedTotalCost = expectedItemsCost + expectedDeliveryCost;

            // Act
            Order order = orderManager.CreateOrder(items, "ул. Мира, д. 10", deliveryService);

            // Assert
            Assert.AreEqual(expectedTotalCost, order.TotalCost);
        }

        [TestMethod]
        public void CreateOrder_WithExpressDeliveryAndDiscount_CalculatesCorrectTotalCost()
        {
            // Arrange
            var builder = new OrderBuilder();
            var discountStrategy = new DiscountCostStrategy(15); // 15% скидка
            var orderManager = new OrderManager(builder, discountStrategy);
            var items = new MenuItem[] { new MenuItem("Роллы 'Филадельфия'", 1200), new MenuItem("Салат 'Цезарь'", 300) };
            var deliveryService = new ExpressDelivery();
            decimal itemsCost = 1500;
            decimal discountedItemsCost = itemsCost * 0.85m;
            decimal deliveryCost = discountedItemsCost * 0.2m;
            decimal expectedTotalCost = discountedItemsCost + deliveryCost;

            // Act
            Order order = orderManager.CreateOrder(items, "пр. Победы, д. 5", deliveryService);

            // Assert
            Assert.AreEqual(expectedTotalCost, order.TotalCost);
        }

        [TestMethod]
        public void OrderState_TransitionsCorrectly()
        {
            // Arrange
            var order = new Order();

            // Act & Assert
            Assert.IsInstanceOfType(order.GetStatus(), typeof(string));
            Assert.AreEqual("Заказ готовится", order.GetStatus());

            order.NextState();
            Assert.AreEqual("Заказ доставляется", order.GetStatus());

            order.NextState();
            Assert.AreEqual("Заказ выполнен", order.GetStatus());
        }

        [TestMethod]
        public void OrderObserver_IsNotifiedOnStateChange()
        {
            // Arrange
            var order = new Order();
            var smsNotifier = new SmsNotifier();
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            order.AddObserver(smsNotifier);

            // Act
            order.SetState(new DeliveringState());

            // Assert
            // Проверяем, что в консоль вывелось правильное уведомление
            string expectedOutput = $"СМС-Уведомление: Статус вашего заказа изменен на: '{new DeliveringState().GetStatus(order)}'.{Environment.NewLine}";
            Assert.AreEqual(expectedOutput, stringWriter.ToString());
        }
    }
}