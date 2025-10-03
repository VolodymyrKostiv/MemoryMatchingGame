using MemoryMatchingGame.Core.Enums;
using MemoryMatchingGame.Core.Entities;
using MemoryMatchingGame.Core.Services.Interfaces.Rules;
using System.Collections.ObjectModel;

namespace MemoryMatchingGame.Core.Services.Interfaces;

public interface IMatchChecker
{
    MatchStatus Check(ObservableCollection<Card> flippedCards, IRuleSet settings);
}
