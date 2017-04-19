using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DIPDIIOC.Dependency;
using DIPDIIOC.LifetimeManager.Interface;
using DIPDIIOC.LifetimeManager.Interface.Imp;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace DIPDIIOC
{
    class Program
    {
        public static UnityContainer Container { get; } = new UnityContainer();

        static void Main(string[] args)
        {

            //var om = new OperationMain();
            //om.PlayMedia();

            //
            //UnityContainer Container = new UnityContainer();
            //Container.RegisterType<IMediaFile, MediaFile>();
            //Container.RegisterType<IPlayer, Player>();
            //var om = Container.Resolve<OperationMain>();
            //om.PlayMedia();

            IClass myClass = new MyClass();
            IClass yourClass = new YourClass();
            Container.RegisterInstance<IClass>(myClass);
            Container.RegisterInstance<IClass>("yourClass", yourClass);
            Container.Resolve<IClass>().ShowInfo();
            Container.Resolve<IClass>("yourClass").ShowInfo();

            TransientLifetimeManagerCode();
            Console.WriteLine("");
            ContainerControlledLifetimeManagerCode();
            Console.WriteLine("");
            HierarchicalLifetimeManagerCode();
            Console.WriteLine("");
            PerResolveLifetimeManagerCode();
            Console.WriteLine("");
            PerThreadLifetimeManagerCode();
            //Thread.Sleep原因？？？
            Thread.Sleep(1000);
            Console.WriteLine("");
            ExternallyControlledLifetimeManagerCode();
            Console.WriteLine("");

            //IoC.InitializeWith(new DependencyResolverFactory());
            //IoC.ResolveAll<IView>();

            Console.ReadKey();
        }

        /// <summary>
        /// 瞬态生命周期，默认情况下，在使用RegisterType进行对象关系注册时如果没有指定生命周期管理器则默认使用这个生命周期管理器，这个生命周期管理器就如同其名字一样，当使用这种管理器的时候，每次通过Resolve或ResolveAll调用对象的时候都会重新创建一个新的对象。
        /// </summary>
        static void TransientLifetimeManagerCode()
        {
            //以下2种注册效果是一样的
            Container.RegisterType<IClass, MyClass>();
            Container.RegisterType<IClass, MyClass>(new TransientLifetimeManager());
            Console.WriteLine("-------TransientLifetimeManager Begin------");
            Console.WriteLine("第一次调用RegisterType注册的对象HashCode:" + Container.Resolve<IClass>().GetHashCode());
            Console.WriteLine("第二次调用RegisterType注册的对象HashCode:" + Container.Resolve<IClass>().GetHashCode());
            Console.WriteLine("-------TransientLifetimeManager End------");
        }

        public static void TransientLifetimeManagerConfiguration()
        {
            //获取指定名称的配置节
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            Container.LoadConfiguration(section, "First");

            Console.WriteLine("-------TransientLifetimeManager Begin------");
            Console.WriteLine("第一次调用RegisterType注册的对象HashCode:" + Container.Resolve<IClass>("transient").GetHashCode());
            Console.WriteLine("第二次调用RegisterType注册的对象HashCode:" + Container.Resolve<IClass>("transient").GetHashCode());
            Console.WriteLine("-------TransientLifetimeManager End------");
        }

        /// <summary>
        /// 容器控制生命周期管理，这个生命周期管理器是RegisterInstance默认使用的生命周期管理器，也就是单件实例，UnityContainer会维护一个对象实例的强引用，每次调用的时候都会返回同一对象
        /// </summary>
        public static void ContainerControlledLifetimeManagerCode()
        {
            IClass myClass = new MyClass();
            //以下2种注册效果是一样的
            Container.RegisterInstance<IClass>("ccl", myClass);
            Container.RegisterInstance<IClass>("ccl", myClass, new ContainerControlledLifetimeManager());

            Container.RegisterType<IClass, MyClass>(new ContainerControlledLifetimeManager());
            Console.WriteLine("-------ContainerControlledLifetimeManager Begin------");
            Console.WriteLine("第一次调用RegisterType注册的对象HashCode:" + Container.Resolve<IClass>().GetHashCode());
            Console.WriteLine("第二次调用RegisterType注册的对象HashCode:" + Container.Resolve<IClass>().GetHashCode());
            Console.WriteLine("第一次调用RegisterInstance注册的对象HashCode:" + Container.Resolve<IClass>("ccl").GetHashCode());
            Console.WriteLine("第二次调用RegisterInstance注册的对象HashCode:" + Container.Resolve<IClass>("ccl").GetHashCode());
            Console.WriteLine("-------ContainerControlledLifetimeManager End------");
        }

        /// <summary>
        /// 分层生命周期管理器，这个管理器类似于ContainerControlledLifetimeManager，也是由UnityContainer来管理，也就是单件实例。不过与ContainerControlledLifetimeManager不 同的是，这个生命周期管理器是分层的，因为Unity的容器时可以嵌套的，所以这个生命周期管理器就是针对这种情况，当使用了这种生命周期管理器，父容器 和子容器所维护的对象的生命周期是由各自的容器来管理
        /// </summary>
        public static void HierarchicalLifetimeManagerCode()
        {
            Container.RegisterType<IClass, MyClass>(new HierarchicalLifetimeManager());
            //创建子容器
            var childContainer = Container.CreateChildContainer();
            childContainer.RegisterType<IClass, MyClass>(new HierarchicalLifetimeManager());

            Console.WriteLine("-------ContainerControlledLifetimeManager Begin------");
            Console.WriteLine("第一次调用父容器注册的对象HashCode:" + Container.Resolve<IClass>().GetHashCode());
            Console.WriteLine("第二次调用父容器注册的对象HashCode:" + Container.Resolve<IClass>().GetHashCode());
            Console.WriteLine("第一次调用子容器注册的对象HashCode:" + childContainer.Resolve<IClass>().GetHashCode());
            Console.WriteLine("第二次调用子容器注册的对象HashCode:" + childContainer.Resolve<IClass>().GetHashCode());
            Console.WriteLine("-------ContainerControlledLifetimeManager End------");
        }

        /// <summary>
        /// 这个生命周期是为了解决循环引用而重复引用的生命周期，先看一下微软官方给出的实例
        /// </summary>
        public static void PerResolveLifetimeManagerCode()
        {
            Container.RegisterType<IPresenter, MockPresenter>();
            Container.RegisterType<IView, View>(new PerResolveLifetimeManager());

            var view = Container.Resolve<IView>();
            var tempPresenter = Container.Resolve<IPresenter>();
            var realPresenter = (MockPresenter)view.Presenter;

            Console.WriteLine("-------PerResolveLifetimeManager Begin------");
            Console.WriteLine("使用了PerResolveLifetimeManager的对象 Begin");
            Console.WriteLine("通过Resolve方法获取的View对象：" +view.GetHashCode());
            Console.WriteLine("View对象中的Presenter对象所包含的View对象：" +realPresenter.View.GetHashCode());
            Console.WriteLine("使用了PerResolveLifetimeManager的对象 End");
            Console.WriteLine("");
            Console.WriteLine("未使用PerResolveLifetimeManager的对象 Begin");
            Console.WriteLine("View对象中的Presenter对象：" +realPresenter.GetHashCode());
            Console.WriteLine("通过Resolve方法获取的View对象：" +tempPresenter.GetHashCode());
            Console.WriteLine("未使用PerResolveLifetimeManager的对象 End");
            Console.WriteLine("-------PerResolveLifetimeManager Begin------");
        }

        /// <summary>
        /// 每线程生命周期管理器，就是保证每个线程返回同一实例
        /// </summary>
        public static void PerThreadLifetimeManagerCode()
        {
            Container.RegisterType<IClass, MyClass>(new PerThreadLifetimeManager());
            var thread = new Thread(Thread1);
            Console.WriteLine("-------PerResolveLifetimeManager Begin------");
            Console.WriteLine("默认线程 Begin");
            Console.WriteLine("第一调用:" +Container.Resolve<IClass>().GetHashCode());
            Console.WriteLine("第二调用:" +Container.Resolve<IClass>().GetHashCode());
            Console.WriteLine("默认线程 End");
            thread.Start(Container);
        }
        public static void Thread1(object obj)
        {
            var tmpContainer = obj as UnityContainer;
            Console.WriteLine("新建线程 Begin");
            Console.WriteLine("第一调用:" +tmpContainer.Resolve<IClass>().GetHashCode());
            Console.WriteLine("第二调用:" +tmpContainer.Resolve<IClass>().GetHashCode());
            Console.WriteLine("新建线程 End");
            Console.WriteLine("-------PerResolveLifetimeManager End------");
        }

        /// <summary>
        /// 外部控制生命周期管理器，这个 生命周期管理允许你使用RegisterType和RegisterInstance来注册对象之间的关系，但是其只会对对象保留一个弱引用，其生命周期 交由外部控制，也就是意味着你可以将这个对象缓存或者销毁而不用在意UnityContainer，而当其他地方没有强引用这个对象时，其会被GC给销毁 掉。在默认情况下，使用这个生命周期管理器，每次调用Resolve都会返回同一对象（单件实例），如果被GC回收后再次调用Resolve方法将会重新创建新的对象
        /// </summary>
        public static void ExternallyControlledLifetimeManagerCode()
        {
            Container.RegisterType<IClass, MyClass>(new ExternallyControlledLifetimeManager());
            var myClass1 = Container.Resolve<IClass>();
            var myClass2 = Container.Resolve<IClass>();
            Console.WriteLine("-------ExternallyControlledLifetimeManager Begin------");
            Console.WriteLine("第一次调用：" +myClass1.GetHashCode());
            Console.WriteLine("第二次调用：" +myClass2.GetHashCode());
            myClass1 = myClass2 = null;
            GC.Collect();
            Console.WriteLine("****GC回收过后****");
            Console.WriteLine("第一次调用：" +Container.Resolve<IClass>().GetHashCode());
            Console.WriteLine("第二次调用：" +Container.Resolve<IClass>().GetHashCode());
            Console.WriteLine("-------ExternallyControlledLifetimeManager End------");
        }
    }
}
