namespace DIPDIIOC.LifetimeManager.Interface.Imp
{
    public class MockPresenter:IPresenter
    {
        public IView View { get; set; }
        public MockPresenter(IView view)
        {
            View = view;
        }
    }
}