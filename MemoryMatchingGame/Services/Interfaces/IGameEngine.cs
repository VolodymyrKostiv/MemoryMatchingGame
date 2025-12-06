using MemoryMatchingGame.Core.Entities;
using MemoryMatchingGame.Core.Services.Interfaces.Rules;
using System.Collections.ObjectModel;

namespace MemoryMatchingGame.Core.Services.Interfaces;

public interface IGameEngine
{
    void StartNewGame(IRuleSet ruleSet);
    void FlipCard(Card card);
    (int Rows, int Cols) CalculateGrid(int totalCards);
    ObservableCollection<Card>? Cards { get; }  
}
