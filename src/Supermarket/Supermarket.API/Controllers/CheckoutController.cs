using Microsoft.AspNetCore.Mvc;
using Supermarket.Shared.Services;

namespace Supermarket.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ILogger<CheckoutController> _logger;
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ILogger<CheckoutController> logger, ICheckoutService checkoutService)
        {
            _logger = logger;
            _checkoutService = checkoutService;
        }

        [HttpPost("scan")]
        public ActionResult Scan(string sku)
        {
            _logger.LogInformation($"Scan SKU: {sku}");
            _checkoutService.Scan(sku);
            return Ok();
        }

        [HttpGet("total")]
        public ActionResult TotalPrice()
        {
            _logger.LogInformation("Getting total price.");
            return Ok($"Total price is {((double) _checkoutService.GetTotalPrice() / 100).ToString("c")}");
        }

        [HttpPost("reset")]
        public ActionResult Reset()
        {
            _logger.LogInformation("Resetting checkout.");
            _checkoutService.Reset();
            return Ok();
        }
    }
}