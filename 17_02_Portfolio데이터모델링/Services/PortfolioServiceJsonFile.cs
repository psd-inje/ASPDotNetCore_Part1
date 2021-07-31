using _17_02_Portfolio데이터모델링.Models;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PortfolioServiceJsonFile(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName 
        {
            get
            {
                //return this._webHostEnvironment.WebRootPath + "\\Portfolios" + "\\portfolios.json";
                return Path.Combine(this._webHostEnvironment.WebRootPath, "Portfolios", "portfolios.json");
            }
        }


        public IEnumerable<Portfolio> GetPortfolios()
        {
            //var jsonFileName = @"C:\Users\psdin\source\repos\ASPDotNetCore_Part1\17_02_Portfolio데이터모델링\wwwroot\Portfolios\portfolios.json";
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<Portfolio[]>(jsonFileReader.ReadToEnd(), options);

                //var portfolios = JsonSerializer.Deserialize<Portfolio[]>(jsonFileReader.ReadToEnd(), options);
                //return portfolios;
            }

        }


        public void AddRating(int portfolioId, int rating)
        {
            var portfolios = GetPortfolios();

            if(portfolios.First(p => p.Id == portfolioId).Ratings == null)
            {
                portfolios.First(p => p.Id == portfolioId).Ratings = new int[] { rating };
            }
            else
            {
                var ratings = portfolios.First(p => p.Id == portfolioId).Ratings.ToList();
                ratings.Add(rating);
                portfolios.First(p => p.Id == portfolioId).Ratings = ratings.ToArray();
            }

            using var outputStream = File.OpenWrite(JsonFileName);
            JsonSerializer.Serialize<IEnumerable<Portfolio>>(
                new Utf8JsonWriter(outputStream, new JsonWriterOptions
                {
                    SkipValidation = true,
                    Indented = true
                }), portfolios);
        }
    }
}
