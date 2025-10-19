using System.Diagnostics;
using GYMManagementBLL.Services.Interfaces;
using GYMManagementPL.Models;
using Microsoft.AspNetCore.Mvc;

namespace GYMManagementPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalyticsDataService _analyticsDataService;

        public HomeController(IAnalyticsDataService analyticsDataService)
        {
            _analyticsDataService = analyticsDataService;
        }

        public ActionResult Index()
        {
            var analyticsData = _analyticsDataService.GetAnalyticsData();
            return View(analyticsData);
        }

    }
}
