using MemoryMatchingGame.Core.Models;

namespace MemoryMatchingGame.Core.Services.Interfaces;

public interface IGameEngineFactory
{
    IGameEngine Create(GameRules gameRules);
}
