namespace MemoryMatchingGame.Core.Services.Interfaces.Rules;

public interface IRuleSetProvider
{
    IEnumerable<IRuleSet> GetPredefined();
}
