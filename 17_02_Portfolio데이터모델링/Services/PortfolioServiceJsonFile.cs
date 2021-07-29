using _17_02_Portfolio데이터모델링.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace _17_02_Portfolio데이터모델링.Services
{
    public class PortfolioServiceJsonFile
    {

        public IEnumerable<Portfolio> GetPortfolios()
        {
            var jsonFileName = @"C:\Users\psdin\source\repos\ASPDotNetCore_Part1\17_02_Portfolio데이터모델링\wwwroot\Portfolios\portfolios.json";
            using (var jsonFileReader = File.OpenText(jsonFileName))
            {
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<Portfolio[]>(jsonFileReader.ReadToEnd(), options);

                //var portfolios = JsonSerializer.Deserialize<Portfolio[]>(jsonFileReader.ReadToEnd(), options);
                //return portfolios;
            }

        }
    }
}
