using System;

using Nancy.Hosting.Self;

namespace PCLExt.Nancy
{
    public class DesktopNancy : INancy
    {
        public static NancyData DataApi { get; private set; }

        private static NancyHost Server { get; set; }


        public void SetDataApi(NancyData data) { DataApi = data; }

        public void Start(string url, ushort port)
        {
            var config = new HostConfiguration { RewriteLocalhost = false };

            Server?.Stop();
            Server = new NancyHost(new CustomBootstrapper(), config, new Uri($"http://{url}:{port}/"));
            Server.Start();
        }
        public void Stop() { Server?.Dispose(); }
    }
}
