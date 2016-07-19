using Learn.MvcConsole.Interface;
using Learn.MvcConsole.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System;
using Microsoft.Framework.DependencyInjection;

namespace Learn.MvcConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Method4();

            Console.ReadLine();
        }

        static void Method1()
        {
            IServiceCollection services = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddTransient<IBar, Bar>()
                .AddTransient<IBaz, Baz>()
                .AddTransient<IGux, Gux>();

            IServiceProvider provider = services.BuildServiceProvider();
            System.Console.WriteLine("provider.GetService<IFoo>(): {0}", provider.GetService<IFoo>());
            System.Console.WriteLine("provider.GetService<IBar>(): {0}", provider.GetService<IBar>());
            System.Console.WriteLine("provider.GetService<IBaz>(): {0}", provider.GetService<IBaz>());
            System.Console.WriteLine("provider.GetService<IGux>(): {0}", provider.GetService<IGux>());
        }

        static void Method2()
        {
            new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddTransient<IBar, Bar>()
                .AddTransient<IGux, Gux>()
                .BuildServiceProvider()
                .GetServices<IGux>();
        }

        static void Method3()
        {
            IServiceProvider provider1 = new ServiceCollection().BuildServiceProvider();
            IServiceProvider provider2 = provider1.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;
            //object root = provider2.GetType()
            //    .GetField("_root", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            //    .GetValue(provider2);

            //Debug.Assert(object.ReferenceEquals(provider1, root));
        }

        static void Method4()
        {
            IServiceProvider root = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar, Bar>()
                .AddSingleton<IBaz, Baz>()
                .BuildServiceProvider();

            IServiceProvider child1 = root.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;
            IServiceProvider child2 = root.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;

            Console.WriteLine("ReferenceEquals(root.GetService<IFoo>(), root.GetService<IFoo>() = {0}", ReferenceEquals(root.GetService<IFoo>(), root.GetService<IFoo>()));
            Console.WriteLine("ReferenceEquals(child1.GetService<IBar>(), child1.GetService<IBar>() = {0}", ReferenceEquals(child1.GetService<IBar>(), child1.GetService<IBar>()));
            Console.WriteLine("ReferenceEquals(child1.GetService<IBar>(), child2.GetService<IBar>() = {0}", ReferenceEquals(child1.GetService<IBar>(), child2.GetService<IBar>()));
            Console.WriteLine("ReferenceEquals(child1.GetService<IBaz>(), child2.GetService<IBaz>() = {0}", ReferenceEquals(child1.GetService<IBaz>(), child2.GetService<IBaz>()));
        }
    }
}
