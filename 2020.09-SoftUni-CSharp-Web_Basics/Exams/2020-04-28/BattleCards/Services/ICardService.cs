using System.Collections.Generic;

namespace BattleCards.Services
{
    using ViewModels.Cards;

    public interface ICardService
    {
        public ICollection<CardViewModel> GetAll();

        public int AddCard(CardAddInputModel inputCard);

        public IEnumerable<CardViewModel> GetByUserId(string userId);

        public void AddCardToUserCollection(string userId, int cardId);

        public void RemoveCardFromUserCollection(string userId, int cardId);

    }
}
