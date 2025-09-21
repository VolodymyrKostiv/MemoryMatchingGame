using MemoryMatchingGame.Core.Enums;
using MemoryMatchingGame.Core.Models;
using MemoryMatchingGame.Core.Services.Interfaces;

namespace MemoryMatchingGame.Core.Services.Implementations;

internal class MatchChecker : IMatchChecker
{
    public MatchStatus Check(List<Card> flippedCards, GameRules settings)
    {
        var key = flippedCards.First().MatchingKey;
        if (!flippedCards.All(c => c.MatchingKey == key))
        {
            return MatchStatus.NotMatch;
        }
        else if (flippedCards.Count != settings.CardsToMatch)
        {
            return MatchStatus.PartlyMatched;
        }
        else
        {
            return MatchStatus.AllMatched;
        }
    }
}
