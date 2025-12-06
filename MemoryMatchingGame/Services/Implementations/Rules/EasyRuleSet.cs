using MemoryMatchingGame.Core.Services.Interfaces.Rules;

namespace MemoryMatchingGame.Core.Services.Implementations.Rules;

public sealed class EasyRuleSet : IRuleSet
{
    public static EasyRuleSet Instance { get; } = new();
    private EasyRuleSet() { }

    public int TotalCards => 16;
    public int CardsPerMatch => 2;
    public string Name => $"Easy ({TotalCards} cards, {CardsPerMatch} per match)";
}
