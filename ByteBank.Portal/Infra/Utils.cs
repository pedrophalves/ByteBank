using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infra
{
    public static class Utils
    {
        public static string ConvertPathToAssemblyName(string path)
        {
            return $"ByteBank.Portal{path.Replace('/', '.')}";
        }

        public static string GetContetType(string path)
        {
            if (path.EndsWith(".css")){
                return "text/css; charset=utf-8";
            }

            if (path.EndsWith(".js"))
            {
                return "application/js; charset=utf-8";
            }

            if (path.EndsWith(".html"))
            {
                return "text/html; charset=utf-8";
            }

            throw new NotImplementedException("Content Type not found");
        }
    }
}
