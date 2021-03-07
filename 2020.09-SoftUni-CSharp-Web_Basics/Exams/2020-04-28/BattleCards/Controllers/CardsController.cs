using SUS.HTTP;
using SUS.MvcFramework;

namespace BattleCards.Controllers
{
    using Services;
    using ViewModels.Cards;

    class CardsController : Controller
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            return View(_cardService.GetAll());
        }

        public HttpResponse Collection()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }


            return View(_cardService.GetByUserId(GetUserId()));
        }

        public HttpResponse Add()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Add(CardAddInputModel card)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            var (result, errorMessage) = ValidationHelper.IsValid(card);
            if (!result)
            {
                return Error(errorMessage);
            }

            var cardId = _cardService.AddCard(card);
            _cardService.AddCardToUserCollection(GetUserId(), cardId);
           
            return Redirect("/Cards/All");
        }

        public HttpResponse AddToCollection(int cardId)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            _cardService.AddCardToUserCollection(GetUserId(), cardId);

            return Redirect("/Cards/All");
        }

        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            _cardService.RemoveCardFromUserCollection(GetUserId(), cardId);

            return Redirect("/Cards/Collection");
        }
    }
}
