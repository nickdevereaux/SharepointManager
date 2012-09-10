using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using SPM2.Framework.Configuration;
using SPM2.Framework.Collections;
using System.ComponentModel.Composition;

namespace SPM2.Framework

{
    public class CompositionProvider
    {

        private static object lockObject = new object();
        private static CompositionContainer _current = null; // default(CompositionContainer);




        public static CompositionContainer Current
        {
            get
            {
                if (_current == null)
                {
                    lock (lockObject)
                    {
                        if (_current == null)
                        {
                            _current = Load("addin");
                            
                        }
                    }
                }
                return _current;
            }
        }

        public CompositionProvider()
        {
        }


        public static IEnumerable<Lazy<T>> GetExports<T>(Type contractType)
        {
            string name = AttributedModelServices.GetContractName(contractType);
            return Current.GetExports<T>(name);
        }

        // Gets a ordered collection from the type of T
        public static OrderingCollection<T> GetOrderedExports<T>()
        {
            string name = AttributedModelServices.GetContractName(typeof(T));
            return GetOrderedExports<T>(name);
        }

        public static OrderingCollection<T> GetOrderedExports<T>(Type contractType)
        {
            string name = AttributedModelServices.GetContractName(contractType);
            return GetOrderedExports<T>(name);
        }

        public static OrderingCollection<T> GetOrderedExports<T>(string name)
        {
            OrderingCollection<T> result = new OrderingCollection<T>();

            var exports = CompositionProvider.Current.GetExports<T, IOrderMetadata>(name);

            // Add the items to an ordered list
            foreach (var item in exports)
            {
                result.Add(item);
            }

            return result;
        }

        public static void LoadAssemblies()
        {
            // Force a load of assemblies
            CompositionContainer dummy = Current;
        }


        private static CompositionContainer Load(string addinPath)
        {
            string assmPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //A catalog that can aggregate other catalogs
            var aggrCatalog = new AggregateCatalog();
            
            //An assembly catalog
            var currentAssemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            aggrCatalog.Catalogs.Add(currentAssemblyCatalog);
            
            //var dirCatalog = new DirectoryCatalog(assmPath + "\\" + addinPath, "*.dll");

            aggrCatalog.Catalogs.Add(new DirectoryCatalog(assmPath, "*.dll"));
            aggrCatalog.Catalogs.Add(new DirectoryCatalog(assmPath, "*.exe"));

            //Create a container
            return new CompositionContainer(aggrCatalog);
        }



    }
}
