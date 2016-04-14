using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices.Models.Watson
{
    public class NameValue
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class WatsonProfileResponse
    {
        public List<NameValue> name_values { get; set; }
    }
}
