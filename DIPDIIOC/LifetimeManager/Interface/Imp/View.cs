using Microsoft.Practices.Unity;

namespace DIPDIIOC.LifetimeManager.Interface.Imp
{
    public class View:IView
    {
        [Dependency]
        public IPresenter Presenter { get; set; }
    }
}