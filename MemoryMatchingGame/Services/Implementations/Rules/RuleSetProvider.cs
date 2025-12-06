using MemoryMatchingGame.Core.Services.Interfaces.Rules;

namespace MemoryMatchingGame.Core.Services.Implementations.Rules;

public class RuleSetProvider : IRuleSetProvider
{
    public IEnumerable<IRuleSet> GetPredefined()
    {
        return
        [
            EasyRuleSet.Instance,
            HardRuleSet.Instance,
        ];
    }
}
