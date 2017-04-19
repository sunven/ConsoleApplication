using System;
using System.Collections.Generic;

namespace DIPDIIOC.Dependency
{
    public interface IDependencyResolver:IDisposable
    {
        /// <summary>
        /// 注册 T类型实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void Register<T>(T instance);

        /// <summary>
        /// 注入 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="existing"></param>
        void Inject<T>(T existing);

        /// <summary>
        /// 解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        T Resolve<T>(Type type);

        T Resolve<T>(Type type, string name);

        T Resolve<T>();

        T Resolve<T>(string name);

        IEnumerable<T> ResolveAll<T>();
    }
}