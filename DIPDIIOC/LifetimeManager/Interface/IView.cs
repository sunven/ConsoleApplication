namespace DIPDIIOC.LifetimeManager.Interface
{
    public interface IView
    {
        IPresenter Presenter { get; set; }
    }
}