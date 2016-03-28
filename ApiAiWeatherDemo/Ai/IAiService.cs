using ApiAiWeatherDemo.Ai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAiWeatherDemo.Ai
{
    public interface IAiService
    {
        QueryResponse Query(string query);
    }
}
