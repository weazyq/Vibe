using Microsoft.AspNetCore.Mvc;
using Vibe.Tools.Result;
using Vibe.VirtualScooter.Modules;

public class HomeController : Controller
{
    public HomeController() { }

    [HttpGet("PrintScooterInfo")]
    public void PrintScooterInfo()
    {
        VirtualScooter.Instance.PrintVirtualScooterInfo();
    }

    [HttpGet("CheckScooterAvailability")]
    public Result CheckScooterAvailability()
    {
        return VirtualScooter.Instance.CheckScooterAvailability();
    }

    [HttpGet("ScooterMove")]
    public void ScooterMove(Double x, Double y)
    {
        VirtualScooter.Instance.MoveTo(x, y);
    }
}