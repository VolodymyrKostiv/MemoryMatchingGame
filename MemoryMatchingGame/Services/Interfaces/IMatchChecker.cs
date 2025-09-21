using MemoryMatchingGame.Core.Enums;
using MemoryMatchingGame.Core.Models;

namespace MemoryMatchingGame.Core.Services.Interfaces;

public interface IMatchChecker
{
    MatchStatus Check(List<Card> flippedCards, GameRules settings);
}
