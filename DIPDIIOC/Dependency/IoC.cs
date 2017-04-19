using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DIPDIIOC.Dependency
{
    public static class IoC
    {
        //解析器
        private static IDependencyResolver _resolver;

        /// <summary>
        /// 初始化，创建实例对象
        /// </summary>
        /// <param name="factory"></param>
        public static void InitializeWith(IDependencyResolverFactory factory)
        {
            _resolver = factory.CreateInstance();
        }

        /// <summary>
        /// 注册对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public static void Register<T>(T instance)
        {
            _resolver.Register(instance);
        }

        /// <summary>
        /// 注入对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="existing"></param>
        public static void Inject<T>(T existing)
        {
            _resolver.Inject(existing);
        }

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T Resolve<T>(Type type)
        {
            return _resolver.Resolve<T>(type);
        }
        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Resolve<T>(Type type, string name)
        {
            return _resolver.Resolve<T>(type, name);
        }
        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return _resolver.Resolve<T>();
        }
        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Resolve<T>(string name)
        {
            return _resolver.Resolve<T>(name);
        }
        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ResolveAll<T>()
        {
            return _resolver.ResolveAll<T>();
        }
        /// <summary>
        /// 销毁
        /// </summary>
        public static void Reset()
        {
            if (_resolver != null)
            {
                _resolver.Dispose();
            }
        }
    }
}