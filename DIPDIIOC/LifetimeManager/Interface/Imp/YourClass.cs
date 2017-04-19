using System;

namespace DIPDIIOC.LifetimeManager.Interface.Imp
{
    public class YourClass : IClass
    {
        public string Name { get; set; } = "YourClassName";

        public void ShowInfo()
        {
            Console.WriteLine(Name);
        }
    }
}