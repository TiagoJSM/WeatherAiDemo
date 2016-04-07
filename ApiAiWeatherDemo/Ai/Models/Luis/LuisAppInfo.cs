using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAiWeatherDemo.Ai.Models.Luis
{
    public class LuisAppInfo
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Culture { get; set; }
        public Boolean Active { get; set; }
        public String CreatedDate { get; set; }
        public String ModifiedDate { get; set; }
        public String PublishDate { get; set; }
        public String URL { get; set; }
        public String AuthKey { get; set; }
        public int NumberOfIntents { get; set; }
        public int NumberOfEntities { get; set; }
        public Boolean IsTrained { get; set; }
    }
}