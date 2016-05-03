namespace PCLExt.Nancy
{
    public class EmptyNancy : INancy
    {
        public static NancyData DataApi { get; private set; }


        public void SetDataApi(NancyData data) { DataApi = data; }

        public void Start(string url, ushort port) { }
        public void Stop() { }
    }
}
