using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Permission permission = Permission.create | Permission.read | Permission.update | Permission.delete;
            Console.WriteLine("1、枚举创建，并赋值……");
            Console.WriteLine(permission.ToString());
            Console.WriteLine((int)permission);

            permission = (Permission)Enum.Parse(typeof(Permission), "5");
            Console.WriteLine("2、通过数字字符串转换……");
            Console.WriteLine(permission.ToString());
            Console.WriteLine((int)permission);

            permission = (Permission)Enum.Parse(typeof(Permission), "update, delete, read", true);
            Console.WriteLine("3、通过枚举名称字符串转换……");
            Console.WriteLine(permission.ToString());
            Console.WriteLine((int)permission);

            permission = (Permission)7;
            Console.WriteLine("4、直接用数字强制转换……");
            Console.WriteLine(permission.ToString());
            Console.WriteLine((int)permission);

            permission = permission & ~Permission.read;
            Console.WriteLine("5、去掉一个枚举项……");
            Console.WriteLine(permission.ToString());
            Console.WriteLine((int)permission);

            permission = permission | Permission.delete;
            Console.WriteLine("6、加上一个枚举项……");
            Console.WriteLine(permission.ToString());
            Console.WriteLine((int)permission);
            Console.ReadKey();
        }
    }

    [Flags]
    public enum Permission
    {
        create = 1,
        read = 2,
        update = 4,
        delete = 8,
    }
}
