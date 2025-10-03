namespace MemoryMatchingGame.Core.Services.Interfaces.Rules;

public interface IRuleSetConstraints
{
    int MinCardsPerMatch { get; }
    int MaxCardsPerMatch { get; }
    int MinTotalCards { get; }
    int MaxTotalCards { get; }
    bool Validate(IRuleSet ruleSet, out string errorMessage);
}
