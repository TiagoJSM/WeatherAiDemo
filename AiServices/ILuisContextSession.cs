using AiServices.Models.Luis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices
{
    public interface ILuisContextSession
    {
        void SaveContext(LuisContext context);
        LuisContext GetContext();
    }
}
