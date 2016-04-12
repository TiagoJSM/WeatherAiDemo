using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AiServices.Models.Luis
{
    public class Metadata
    {
        public Boolean hasSpace { get; set; }
    }

    public class IntentResult
    {
        public String name { get; set; }
        public String label { get; set; }
        public float score { get; set; }
    }

    public class Indece
    {
        public int StartToken { get; set; }
        public int endToken { get; set; }
    }

    public class EntityResult
    {
        public String name { get; set; }
        public Indece indeces { get; set; }
        public String work { get; set; }
        public String color { get; set; }
        public Boolean isBuiltInExtractor { get; set; }
    }

    public class LuisIntentSample
    {
        public String utteranceText { get; set; }
        public List<String> tokenizedText { get; set; }
        public int exampleID { get; set; }
        public List<IntentResult> intentResults { get; set; }
        public List<EntityResult> entitiesResults { get; set; }
        public List<Metadata> metadata { get; set; }
    }
}