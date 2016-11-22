namespace Base.Prism.Interfaces
{
    public interface IStateNavigationService<T> : INavigationService<T>
    {
        bool CanNext { get; }
        bool CanBack { get; }

        void Next();

        void Back();
    }
}
