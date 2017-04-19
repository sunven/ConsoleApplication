using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace DIPDIIOC.Dependency
{
    public class UnityDependencyResolver: DisposableResource,IDependencyResolver
    {
        //注入容器
        private readonly IUnityContainer _container;

        
        public UnityDependencyResolver() : this(new UnityContainer())
        {
            var configuration = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            configuration.Containers.Default.Configure(_container);
        }

        
        public UnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        
        public void Register<T>(T instance)
        {
            _container.RegisterInstance(instance);
        }

        
        public void Inject<T>(T existing)
        {
            //注入加载
            _container.BuildUp(existing);
        }

        
        public T Resolve<T>(Type type)
        {
            //解析
            return (T)_container.Resolve(type);
        }

        
        public T Resolve<T>(Type type, string name)
        {
            return (T)_container.Resolve(type, name);
        }

        
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        
        public T Resolve<T>(string name)
        {
            return _container.Resolve<T>(name);
        }

        
        public IEnumerable<T> ResolveAll<T>()
        {
            //解析容器中所有
            IEnumerable<T> namedInstances = _container.ResolveAll<T>();
            T unnamedInstance = default(T);

            try
            {
                unnamedInstance = _container.Resolve<T>();
            }
            catch (ResolutionFailedException)
            {
                //When default instance is missing
            }

            if (Equals(unnamedInstance, default(T)))
            {
                return namedInstances;
            }

            return new ReadOnlyCollection<T>(new List<T>(namedInstances) { unnamedInstance });
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _container.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}