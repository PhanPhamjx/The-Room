namespace Project_TR.Events
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}