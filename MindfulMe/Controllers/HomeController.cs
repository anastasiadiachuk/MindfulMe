using Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;
using System.Security.Claims;

namespace MindfulMe.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IOfferService _offerService;

        public HomeController(IArticleService articleService, IOfferService offerService)
        {
            _articleService = articleService;
            _offerService = offerService;
        }

        [Route("/")]
        [Route("/index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/view_articles")]
        public IActionResult ViewArticles()
        {
            return View(_articleService.GetAllArticles());
        }

        [HttpGet]
        [Route("/add_offer")]
        public IActionResult AddOffer()
        {
            return View();
        }

        [HttpPost]
        [Route("/add_offer")]
        public IActionResult AddOffer(Offer offer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _offerService.CreateOffer(offer);
                    return RedirectToAction("ViewArticles");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(offer);
        }

        [HttpGet]
        [Route("/view_offers")]
        public IActionResult ViewOffers()
        {
            return View(_offerService.GetAllOffers());
        }

        [HttpGet]
        [Route("/add_article")]
        public IActionResult AddArticle()
        {
            return View();
        }

        [HttpPost]
        [Route("/add_article")]
        public IActionResult AddArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _articleService.CreateArticle(article);
                    return RedirectToAction("ViewArticles");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(article);
        }

        [HttpPost]
        [Route("/article/read")]
        public IActionResult ReadArticle(int id)
        {
            var article = _articleService.GetArticleById(id);

            if (article != null)
            {
                return View(article);
            }

            return NotFound();
        }
    }
}
