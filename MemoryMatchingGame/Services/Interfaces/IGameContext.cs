using MemoryMatchingGame.Core.Services.Interfaces.Rules;

namespace MemoryMatchingGame.Core.Services.Interfaces;

public interface IGameContext
{
    IRuleSet CurrentRuleSet { get; set; }
}
