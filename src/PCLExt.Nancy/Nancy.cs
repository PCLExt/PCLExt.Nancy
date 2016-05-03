using System;

#if DESKTOP || MAC
using PCLExt.FileStorage;

using Nancy;
using Nancy.ErrorHandling;
#endif

namespace PCLExt.Nancy
{
#if DESKTOP || MAC
    public class CustomStatusCode : IStatusCodeHandler
    {
        public void Handle(HttpStatusCode statusCode, NancyContext context) { context.Response.StatusCode = HttpStatusCode.Forbidden; }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context) =>
                statusCode == HttpStatusCode.NotFound || statusCode == HttpStatusCode.InternalServerError ||
                statusCode == HttpStatusCode.Forbidden || statusCode == HttpStatusCode.Unauthorized;
    }
    public class CustomRootPathProvider : IRootPathProvider
    {
        public string GetRootPath() => Storage.ContentFolder.Path;
    }
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override IRootPathProvider RootPathProvider => new CustomRootPathProvider();
    }

    public class ApiNancyModule : NancyModule
    {
        public ApiNancyModule() : base("/api")
        {
            foreach (var pageAction in DesktopNancy.DataApi.List)
                Get[$"/{pageAction.Page}"] = pageAction.Action;
        }
    }
#endif

    /// <summary>
    /// 
    /// </summary>
    public static class Nancy
    {
        private static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException(@"This functionality is not implemented in the portable version of this assembly.
You should reference the PCLExt.Nancy NuGet package from your main application project in order to reference the platform-specific implementation.");

        private static INancy _nancy;
        private static INancy Instance
        {
            get
            {
#if DESKTOP || MAC
                if (_nancy == null)
                    _nancy = new DesktopNancy();

                return _nancy;
#elif ANDROID || __IOS__
                if (_nancy == null)
                    _nancy = new EmptyNancy();

                return _nancy;
#endif

                throw NotImplementedInReferenceAssembly();
            }
        }

        public static void SetDataApi(NancyData data) { Instance.SetDataApi(data); }

        public static void Start(string url, ushort port) { Instance.Start(url, port); }

        public static void Stop() { Instance.Stop(); }
    }
}
