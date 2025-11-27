namespace DeliveryService.Patterns.Observer
{
    public interface IOrderSubject
    {
        void AddObserver(IOrderObserver observer);
        void RemoveObserver(IOrderObserver observer);
        void NotifyObservers();
    }
}