namespace DIPDIIOC.Dependency
{
    public interface IDependencyResolverFactory
    {
        /// <summary>
        /// 创建IDependencyResolver的实例
        /// </summary>
        /// <returns></returns>
        IDependencyResolver CreateInstance();
    }
}