using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ClockingApp.Models;
using ClockingApp.CustomServices;
using ClockingApp.Models.ClockingData;

namespace ClockingApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ClockingService _clockingService;

    public HomeController(ILogger<HomeController> logger, ClockingService clockingService)
    {
        _logger = logger;
        _clockingService = clockingService;
    }
    
    public async Task<IActionResult> Index()
    {
        string username = "gomezr";
        int currentWeek = ISOWeek.GetWeekOfYear(DateTime.Now.Date);
        List<Clocking> weekClockings = (await _clockingService._clockingRepo.FindAllAsync(clocking => clocking.Username.Equals(username) && clocking.ClockingWeek.Equals(currentWeek))).ToList();
        Clocking? todaysClocking = weekClockings.Where(clocking => clocking.ClockingDate.Equals(DateTime.Now.Date)).FirstOrDefault();
        ViewData["todaysClocking"] = todaysClocking;
        return View(weekClockings);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

