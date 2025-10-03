using MemoryMatchingGame.Core.Enums;
using MemoryMatchingGame.Core.Entities;
using MemoryMatchingGame.Core.Services.Interfaces.Rules;
using MemoryMatchingGame.Core.Services.Interfaces;
using System.Collections.ObjectModel;

namespace MemoryMatchingGame.Core.Services.Implementations;

public class MatchChecker : IMatchChecker
{
    public MatchStatus Check(ObservableCollection<Card> flippedCards, IRuleSet settings)
    {
        var key = flippedCards.First().MatchingKey;
        if (!flippedCards.All(c => c.MatchingKey == key))
        {
            return MatchStatus.NotMatch;
        }
        else if (flippedCards.Count != settings.CardsPerMatch)
        {
            return MatchStatus.PartlyMatched;
        }
        else
        {
            return MatchStatus.AllMatched;
        }
    }
}
