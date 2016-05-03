using System;

namespace PCLExt.Nancy
{
    public interface INancy
    {
        void SetDataApi(NancyData data);

        void Start(String url, UInt16 port);
        void Stop();
    }
}