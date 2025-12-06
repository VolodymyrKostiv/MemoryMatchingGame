using MemoryMatchingGame.Core.Services.Implementations.Rules;
using MemoryMatchingGame.Core.Services.Interfaces;
using MemoryMatchingGame.Core.Services.Interfaces.Rules;

namespace MemoryMatchingGame.Core.Services.Implementations;

public class GameContext : IGameContext
{
    public IRuleSet CurrentRuleSet { get; set; } = EasyRuleSet.Instance;
}
