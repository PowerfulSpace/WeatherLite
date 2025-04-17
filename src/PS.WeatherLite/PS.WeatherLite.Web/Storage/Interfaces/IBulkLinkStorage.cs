﻿namespace PS.WeatherLite.Web.Storage.Interfaces
{
    public interface IBulkLinkStorage : ILinkStorage
    {
        Dictionary<string, string> GetAll();
    }
}
