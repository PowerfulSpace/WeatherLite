using PS.WeatherLite.Web.Utils.Interfaces;

namespace PS.WeatherLite.Web.Utils
{
    public class PathService : IPathService
    {
        public string GetDataFilePath(string fileName)
        {
            string basePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\.."));
            string dataPath = Path.Combine(basePath, "Data");

            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);

            return Path.Combine(dataPath, fileName);
        }
    }
}
