using MemoryMatchingGame.Core.Models;

namespace MemoryMatchingGame.Core.Services.Interfaces;

public interface IScoreCalculator
{
    bool Calculate(List<Card> flippedCards, bool isMatched);
}
