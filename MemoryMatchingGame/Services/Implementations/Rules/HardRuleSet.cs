using MemoryMatchingGame.Core.Services.Interfaces.Rules;

namespace MemoryMatchingGame.Core.Services.Implementations.Rules;

public sealed class HardRuleSet : IRuleSet
{
    public static HardRuleSet Instance { get; } = new();
    private HardRuleSet() { }

    public int TotalCards => 64;
    public int CardsPerMatch => 4;
    public string Name => $"Hard ({TotalCards} cards, {CardsPerMatch} per match)";
}
