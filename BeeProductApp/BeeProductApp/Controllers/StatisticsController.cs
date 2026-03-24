using BeeProductApp.Models.Statistic;

using Microsoft.AspNetCore.Mvc;

using BeeProductApp.Models.Statistic;
using BeeProductApp.Core.Contracts;

public class StatisticsController : Controller
{
    private readonly IStatisticService statisticsService;

    public StatisticsController(IStatisticService statisticsService)
    {
        this.statisticsService = statisticsService;
    }

    public IActionResult Index()
    {
        StatisticVM statistics = new StatisticVM();

        statistics.CountClients = statisticsService.CountClients();
        statistics.CountProducts = statisticsService.CountProducts();
        statistics.CountOrders = statisticsService.CountOrders();
        statistics.SumOrders = statisticsService.SumOrders();

        return View(statistics);
    }
}

