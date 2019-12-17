using DestinaMarket.Database;
using DestinaMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinaMarket.Services
{
    public class ConfigurationsService
    {
        //public static ConfigurationsService ClassObject {
        //    get {
        //        if (privateInMemoryObject == null) privateInMemoryObject = new ConfigurationsService();

        //        return privateInMemoryObject;
        //    }
        //}
        //private static ConfigurationsService privateInMemoryObject { get; set; }

        //private ConfigurationsService()
        //{
        //}


        public Config GetConfig(string Key)
        {
            using (DMContext context=new DMContext())
            {
                return context.Configurations.Find(Key);
            }
        }
    }
}
