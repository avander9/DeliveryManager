using System.Windows.Forms;
using Autofac;
using TransportesArenas.DeliveryManager.Backend.Implementations;

namespace TransportesArenas.DeliveryManager.App
{
    public static class IocAppContainer
    {
        internal static IContainer Build()  
        {  
            var builder = new ContainerBuilder();  
            RegisterTypes(builder);  
            return builder.Build();  
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            //builder.RegisterType<IoCBackendBuilder>();



        }
    }
}