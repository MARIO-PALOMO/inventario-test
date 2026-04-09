using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario.Infrastructure.Registration
{
    public class ConfigurationManager
    {
        public static IConfiguration? Configuration { get; set; }
        public static string DataBase
        {
            get
            {
                return Configuration != null ? Configuration["ConnectionStrings:InventoryDB"]! : string.Empty;
            }
        }
    }
}
