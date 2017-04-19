using System;
using System.Configuration;

namespace DIPDIIOC.Dependency
{
    public class DependencyResolverFactory:IDependencyResolverFactory
    {
        private readonly Type _resolverType;

        public DependencyResolverFactory(string resolverTypeName)
        {
            //GetType(名字，是否异常，是否区分大小)
            _resolverType = Type.GetType(resolverTypeName, true, true);
        }

        public DependencyResolverFactory() : this(ConfigurationManager.AppSettings["dependencyResolverTypeName"])
        {
        }

        public IDependencyResolver CreateInstance()
        {
            //根据类型创建实例对象
            return Activator.CreateInstance(_resolverType) as IDependencyResolver;
        }
    }
}