using MemoryMatchingGame.Core.Services.Interfaces.Rules;

namespace MemoryMatchingGame.Core.Services.Implementations.Rules;

public sealed class RuleSetConstraints : IRuleSetConstraints
{
    public int MinCardsPerMatch => 2;
    public int MaxCardsPerMatch => 5;
    public int MinTotalCards => 8;
    public int MaxTotalCards => 64;

    public bool Validate(IRuleSet ruleSet, out string errorMessage)
    {
        bool checkResult = true;
        errorMessage = string.Empty;

        if (ruleSet.CardsPerMatch < MinCardsPerMatch || ruleSet.CardsPerMatch > MaxCardsPerMatch)
        {
            errorMessage += $"Number of Cards Per Match should be between {MinCardsPerMatch} and {MaxCardsPerMatch}. ";
            checkResult = false;
        }
        if (ruleSet.TotalCards < MinTotalCards || ruleSet.TotalCards > MaxTotalCards)
        {
            errorMessage += $"Total Cards should be between {MinTotalCards} and {MaxTotalCards}. ";
            checkResult = false;
        }
        if (ruleSet.TotalCards < ruleSet.CardsPerMatch)
        {
            errorMessage += $"Total Cards can't be lower than Cards Per Match. ";
            checkResult = false;
        }
        
        return checkResult;
    }
}
