using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AiServices.Services.Forecast.Models
{
    public class Location
    {
        public String name { get; set; }
        public String region { get; set; }
        public String country { get; set; }
        public int lat { get; set; }
        public int lon { get; set; }
        public String tz_id { get; set; }
        public long localtime_epoch { get; set; }
        public String localtime { get; set; }
    }

    public class Current
    {
        public String last_updated_epoch { get; set; }
        public String last_updated { get; set; }
        public int temp_c { get; set; }
        public int temp_f { get; set; }
        public Condition condition { get; set; }
        public int wind_mph { get; set; }
        public int wind_kph { get; set; }
        public int wind_degree { get; set; }
        public String wind_dir { get; set; }
        public int pressure_md { get; set; }
        public int pressure_in { get; set; }
        public int precip_mm { get; set; }
        public int precip_in { get; set; }
        public int humidity { get; set; }
        public int cloud { get; set; }
        public int feelslike_c { get; set; }
        public int feelslike_f { get; set; }
    }

    public class Condition
    {
        public String text { get; set; }
        public String icon { get; set; }
        public int code { get; set; }
    }

    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class CityForecastModel
    {
        public Location location { get; set; }
        public Current current { get; set; }
        public Error error { get; set; }
    }
}