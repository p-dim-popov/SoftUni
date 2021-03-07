using System.Collections.Generic;
using System.Linq;

namespace BattleCards.Services
{
    using Data;
    using Models;
    using ViewModels.Cards;

    class CardService : ICardService
    {
        private readonly ApplicationDbContext _dbContext;

        public CardService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<CardViewModel> GetAll()
            => _dbContext.Cards
                .Select(c => new CardViewModel
                {
                    Id = c.Id,
                    Attack = c.Attack,
                    Description = c.Description,
                    Health = c.Health,
                    ImageUrl = c.ImageUrl,
                    Keyword = c.Keyword,
                    Name = c.Name,
                })
                .ToList();

        public int AddCard(CardAddInputModel inputCard)
        {
            var card = new Card
            {
                Attack = inputCard.Attack,
                Health = inputCard.Health,
                Description = inputCard.Description,
                ImageUrl = inputCard.Image,
                Keyword = inputCard.Keyword,
                Name = inputCard.Name,
            };

            _dbContext.Cards.Add(card);
            _dbContext.SaveChanges();

            return card.Id;
        }

        public IEnumerable<CardViewModel> GetByUserId(string userId)
            => _dbContext.UserCards
                .Where(c => c.UserId == userId)
                .Select(uc => new CardViewModel
                {
                    Id = uc.CardId,
                    Attack = uc.Card.Attack,
                    Description = uc.Card.Description,
                    Health = uc.Card.Health,
                    ImageUrl = uc.Card.ImageUrl,
                    Keyword = uc.Card.Keyword,
                    Name = uc.Card.Name,
                })
                .ToList();

        public void AddCardToUserCollection(string userId, int cardId)
        {
            if (_dbContext.UserCards.Any(uc => uc.UserId == userId && uc.CardId == cardId))
            {
                return;
            }

            _dbContext.UserCards.Add(new UserCard
            {
                CardId = cardId,
                UserId = userId,
            });

            _dbContext.SaveChanges();
        }

        public void RemoveCardFromUserCollection(string userId, int cardId)
        {
            var link = _dbContext.UserCards
                .FirstOrDefault(uc => uc.UserId == userId && uc.CardId == cardId);
            if (link is null)
            {
                return;
            }

            _dbContext.UserCards.Remove(link);
            _dbContext.SaveChanges();
        }
    }
}
