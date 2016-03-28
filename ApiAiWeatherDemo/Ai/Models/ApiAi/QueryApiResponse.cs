using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAiWeatherDemo.Ai.Models.ApiAi
{
    public class Parameters
    {
        [DeserializeAs(Name = "geo-city")]
        public string GeoCity { get; set; }
    }

    public class Metadata
    {
        public string IntentId { get; set; }
        public string IntentName { get; set; }
    }

    public class Fulfillment
    {
        public string Speech { get; set; }
    }

    public class Result
    {
        public string Source { get; set; }
        public string ResolvedQuery { get; set; }
        public string Action { get; set; }
        public bool ActionIncomplete { get; set; }
        public Parameters Parameters { get; set; }
        public Metadata Metadata { get; set; }
        public Fulfillment Fulfillment { get; set; }
        public int Score { get; set; }
    }

    public class Status
    {
        public int Code { get; set; }
        public string ErrorType { get; set; }
    }

    public class QueryApiResponse
    {
        public string Id { get; set; }
        public string Timestamp { get; set; }
        public Result Result { get; set; }
        public Status Status { get; set; }
    }
}