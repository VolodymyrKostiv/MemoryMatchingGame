using MemoryMatchingGame.Core.Entities;
using MemoryMatchingGame.Core.Services.Interfaces;

namespace MemoryMatchingGame.Core.Services.Implementations;

public class Shuffler : IShuffler
{
    // Based on Fisher–Yates shuffle:
    // http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
    public void FisherYatesShuffle(List<Card> cards)
    {
        var r = new Random();
        for (int n = cards.Count - 1; n > 0; --n)
        {
            int k = r.Next(n + 1);
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }
    }
}
