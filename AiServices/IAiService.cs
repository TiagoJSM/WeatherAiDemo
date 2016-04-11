using ApiAiWeatherDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices.Ai
{
    public interface IAiService<TQueryResponse>
    {
        TQueryResponse Query(string question);
    }
}
