//using System.Reflection;
//using System.Runtime.Loader;
//using FantasyHomeCenter.Application.PluginCenter;
//using FantasyHomeCenter.DevicePluginInterface;

//namespace BlazorApp1;


//public class PluginService
//{
//    public void AddPluginAsync(string path, string name)
//    {
//        try
//        {
//            PluginModel pm = new PluginModel();
//            AssemblyLoadContext pluginLoadContext = new AssemblyLoadContext(path, true);
//            pm.AssemblyContext = pluginLoadContext;


//            var ass = pluginLoadContext.LoadFromAssemblyName(new AssemblyName(name));
//            foreach (Type type in ass.GetTypes())
//            {
//                if (typeof(IDeviceController).IsAssignableFrom((Type)type))
//                {
//                    IDeviceController controller = Activator.CreateInstance(type) as IDeviceController;
//                    if (controller != null)
//                    {
//                        pm.Controller = controller;
//                        pm.Key = controller.Key;
                      
//                    }
//                }
//            }
           
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e);
//            throw;
//        }

//    }
//}