using DeliveryService.Patterns.State;
using DeliveryService.Patterns.Observer;
using System.Collections.Generic;

namespace DeliveryService.Models
{
    public class Order : IOrderSubject
    {
        private readonly List<IOrderObserver> _observers = new List<IOrderObserver>();
        private IOrderState _state;

        public List<MenuItem> Items { get; } = new List<MenuItem>();
        public string DeliveryAddress { get; set; }
        public Patterns.FactoryMethod.DeliveryService DeliveryType { get; set; }
        public decimal TotalCost { get; set; }

        public Order()
        {
            // Изначально заказ находится в состоянии подготовки
            _state = new PreparingState();
        }

        public void SetState(IOrderState state)
        {
            _state = state;
            NotifyObservers(); // Уведомляем наблюдателей о смене состояния
        }

        public string GetStatus()
        {
            return _state.GetStatus(this);
        }

        public void NextState()
        {
            _state.NextState(this);
        }

        public void PreviousState()
        {
            _state.PreviousState(this);
        }

        public void AddObserver(IOrderObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IOrderObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
    }
}