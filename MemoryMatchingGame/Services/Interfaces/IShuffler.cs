using MemoryMatchingGame.Core.Entities;

namespace MemoryMatchingGame.Core.Services.Interfaces;

public interface IShuffler
{
    void FisherYatesShuffle(List<Card> cards);
}
