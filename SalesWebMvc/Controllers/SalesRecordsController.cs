using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        //construtor
        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            //definindo um valor valor padrão para a data minima, caso não tenha
            //no caso é o primeiro dia do ano atual
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            //definindo um valor valor padrão para a data maxima, caso não tenha
            //vai utilizar a data atual
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            //vamos passar os valores das datas utilizado o viewdata[]
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);

            return View(result);
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
