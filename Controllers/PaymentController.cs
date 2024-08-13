using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using StripeWebApp.Data;
using StripeWebApp.Models;

namespace StripeWebApp.Controllers
{
    public class PaymentController : Controller
    {
        protected readonly DbCont _DbCont;

        public PaymentController( DbCont DbCont)
        {
            _DbCont = DbCont;
        }

    public  async Task<IActionResult> Index()
        {
            return View(await _DbCont.Items.ToListAsync());
        }
        [HttpPost]
        public ActionResult CreateCheckout([Bind("Id, Name, ImageUrl, PriceId")] Item item)
        {
            var domain = "https://localhost:7132";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    
                    Price = $"{item.PriceId}",
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = domain + "/Payment/Success",
                CancelUrl = domain + "/Cancel/Success",
            };
            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Cancel()
        {
            return View();
        }

    }
}
