using _17_02_Portfolio데이터모델링.Models;
using _17_02_Portfolio데이터모델링.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace _17_02_Portfolio데이터모델링.Pages.Portfolios
{
    public class IndexModel : PageModel
    {
        private readonly PortfolioServiceJsonFile _service;

        public IndexModel(PortfolioServiceJsonFile service)
        {
            this._service = service;
        }

        public IEnumerable<Portfolio> portfolios { get; private set; }


        public void OnGet()
        {
            portfolios = this._service.GetPortfolios();
        }
    }
}
