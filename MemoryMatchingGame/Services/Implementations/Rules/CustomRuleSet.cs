using MemoryMatchingGame.Core.Services.Interfaces.Rules;

namespace MemoryMatchingGame.Core.Services.Implementations.Rules;

public record CustomRuleSet(int TotalCards, int CardsPerMatch) : IRuleSet
{
    public string Name => $"Custom ({TotalCards} cards, {CardsPerMatch} per match)";
}
