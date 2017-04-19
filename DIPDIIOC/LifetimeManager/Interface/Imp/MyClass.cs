using System;

namespace DIPDIIOC.LifetimeManager.Interface.Imp
{
    public class MyClass : IClass
    {
        public string Name { get; set; } = "MyClassName";

        public void ShowInfo()
        {
            Console.WriteLine(Name);
        }
    }
}