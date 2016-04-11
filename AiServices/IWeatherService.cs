using ApiAiWeatherDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices
{
    public interface IWeatherService
    {
        QueryResponse Query(string question);
    }
}
