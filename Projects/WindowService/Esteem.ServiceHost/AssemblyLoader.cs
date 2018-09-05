using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Infrastructure;
using System.Diagnostics;
using System.IO;

namespace LayrCake.WCFServiceHost
{
    public class AssemblyLoader
    {
        private delegate Assembly AssemblyLoadingDelegate(string assemblyFilePath);

        //[STAThread]
        public static Assembly LoadAssembly(string assemblyPath, string assemblyName)
        {
            string assemblyFilePath = assemblyPath + assemblyName;
            Assembly loadingAssembly = null;

            // Make sure we load the assembly containing the custom attribute
            // first! (Loading another assembly within the AssemblyLoadEvent is 
            // probably not a good idea.)
            Type t = typeof(CommonAttributes.InitializeOnLoadAttribute);
            // Now add the handler
            Thread.GetDomain().AssemblyLoad += new AssemblyLoadEventHandler(InitializeTypesOnLoad);

            try
            {
                if (!String.IsNullOrEmpty(Path.GetDirectoryName(assemblyPath)))
                {
                    assemblyFilePath = assemblyFilePath.EndsWith(".dll") ? assemblyFilePath : assemblyFilePath + ".dll";
                    if (File.Exists(assemblyFilePath))
                    {
                        loadingAssembly = Assembly.LoadFrom(assemblyFilePath);
                    }
                    else
                    {
                        EventLogging.WriteEvent("Error loading assembly : " + assemblyName + " from " + assemblyPath);
                    }
                }
            }
            catch
            {
                EventLogging.WriteEvent("Error cache loading assembly : " + assemblyName + " from " + assemblyPath);
            }

            // Test that the assembly loaded correctly
            var assemblyList = System.AppDomain.CurrentDomain.GetAssemblies();
            Debug.Assert(assemblyList != null, "Assembly list is null");
            Debug.Assert(assemblyList.Where(AssemblyNameContains(assemblyName)).FirstOrDefault() != null,
                "Loaded assembly list does not contain " + assemblyName);

            return loadingAssembly;
        }

        static Assembly AssemblyLoading(string assemblyFilePath)
        {
            var assemblyLoaded = Assembly.LoadFrom(assemblyFilePath);
            if (assemblyLoaded == null)
                AssemblyLoading(assemblyFilePath);
            return assemblyLoaded;
        }

        static void InitializeTypesOnLoad(object sender, AssemblyLoadEventArgs args)
        {
            Assembly loaded = args.LoadedAssembly;
            EventLogging.WriteEvent("Trying to InitializeTypesOnLoad : " + loaded.GetName());

            foreach (Type type in loaded.GetTypes())
            {
                if (type.IsDefined(typeof(CommonAttributes.InitializeOnLoadAttribute), false))
                {
                    ConstructorInfo initializer = type.TypeInitializer;
                    if (initializer != null)
                    {
                        initializer.Invoke(null, null);
                        EventLogging.WriteEvent("Invoking Constructor : " + loaded.GetName());
                    }
                }
            }
            EventLogging.WriteEvent("New Assembly loaded - " + loaded.GetName() + " Time: " + DateTime.Now.ToLongTimeString());
            Console.WriteLine("New Assembly loaded - " + loaded.GetName() + " Time: " + DateTime.Now.ToLongTimeString());
        }

        private static Func<Assembly, bool> AssemblyNameContains(string namePart)
        {
            return u => u.GetName().Name.Contains(namePart);
        }
    }
}
