using MemoryMatchingGame.Core.Services.Interfaces.Rules;

namespace MemoryMatchingGame.Core.Services.Implementations.Rules;

public class HardRuleSet : IRuleSet
{
    public int TotalCards => 64;
    public int CardsPerMatch => 4;
    public string Name => $"Hard ({TotalCards} cards, {CardsPerMatch} per match)";
}
