namespace MemoryMatchingGame.Core.Services.Interfaces.Rules;

public interface IRuleSet
{
    int TotalCards { get; }
    int CardsPerMatch { get; }
    string Name { get; }    
}
