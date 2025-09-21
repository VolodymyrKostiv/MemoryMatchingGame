using MemoryMatchingGame.Core.Models;

namespace MemoryMatchingGame.Core.Services.Interfaces;

public interface IGameEngine
{
    void StartNewGame(GameRules ruleSet);
    void FlipCard(Card card);
}
