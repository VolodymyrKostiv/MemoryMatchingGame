using MemoryMatchingGame.Core.Services.Interfaces.Rules;

namespace MemoryMatchingGame.Core.Services.Implementations.Rules;

public class CustomRuleSet : IRuleSet
{
    public int CardsPerMatch { get; }
    public int TotalCards { get; }
    public string Name => $"Custom ({TotalCards} cards, {CardsPerMatch} per match)";

    public CustomRuleSet(int totalCards, int cardsPerMatch)
    {
        TotalCards = totalCards;
        CardsPerMatch = cardsPerMatch;
    }
}
