using MemoryMatchingGame.Core.Models;
using MemoryMatchingGame.Core.Services.Interfaces;

namespace MemoryMatchingGame.Core.Services.Implementations;

internal class GameEngineFactory : IGameEngineFactory
{
    private readonly IMatchChecker _matchChecker;

    public GameEngineFactory(IMatchChecker matchChecker)
    {
        _matchChecker = matchChecker;
    }

    public IGameEngine Create(GameRules gameRules)
    {
        var engine = new GameEngine(_matchChecker, gameRules);

        return engine;
    }
}
