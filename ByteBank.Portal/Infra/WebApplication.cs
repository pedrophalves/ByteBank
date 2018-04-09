using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infra
{
    public class WebApplication
    {
        private readonly string[] _prefixes;

        public WebApplication(string[] prefixes)
        {
            if (prefixes == null)
            {
                throw new ArgumentNullException(nameof(prefixes));
            }
            _prefixes = prefixes;
        }

        public void Start()
        {
            while (true)
            {
                ManipulateRequest();
            }
        }

        private void ManipulateRequest()
        {
            var httpListener = new HttpListener();

            foreach (var prefixo in _prefixes)
            {
                httpListener.Prefixes.Add(prefixo);
            }

            httpListener.Start();

            var context = httpListener.GetContext();
            var request = context.Request;
            var response = context.Response;

            var path = request.Url.AbsolutePath;
            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = Utils.ConvertPathToAssemblyName(path);

            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            var bytesResource = new byte[resourceStream.Length];

            resourceStream.Read(bytesResource, 0, (int)resourceStream.Length);

            response.ContentType = Utils.GetContetType(path);
            response.StatusCode = 200;
            response.ContentLength64 = resourceStream.Length;

            response.OutputStream.Write(bytesResource, 0, bytesResource.Length);
            response.OutputStream.Close();



            httpListener.Stop();
        }
    }
}
