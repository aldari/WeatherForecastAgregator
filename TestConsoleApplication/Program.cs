using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Services;

namespace TestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var windsorContainer = new WindsorContainer();

            windsorContainer.Install(FromAssembly.Containing<ServiceInstaller>());
            windsorContainer.Register(Component.For<TestDifferentImplementationsOfTheSameInterface>());

            var test = windsorContainer.Resolve<TestDifferentImplementationsOfTheSameInterface>();
            test.Proceed();

            windsorContainer.Dispose();

            Console.WriteLine("Press enter to stop background services...");
            Console.ReadLine();
        }
    }
}
