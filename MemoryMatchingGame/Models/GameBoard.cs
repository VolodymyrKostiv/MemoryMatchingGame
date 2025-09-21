using MemoryMatchingGame.Core.Enums;

namespace MemoryMatchingGame.Core.Models;

internal class GameBoard
{
    private GameRules _gameRules;

    public List<Card> Cards { get; private set; }

    public List<Card> FlippedCards => [.. Cards.Where(c => c.Status == CardStatus.Flipped)];

    public void Initialize(GameRules gameRules)
    {
        _gameRules = gameRules;

        var cards = new List<Card>(gameRules.NumberOfCards);
        int numberOfMatches = gameRules.NumberOfCards / gameRules.CardsToMatch;
        var guidMatches = new Queue<Guid>(numberOfMatches);
        for (int i = 0; i < numberOfMatches; i++)
        {
            var guid = Guid.NewGuid();
            for (int j = 0; j < gameRules.CardsToMatch; j++)
            {
                guidMatches.Enqueue(guid);
            }
        }

        for (int i = 0; i < gameRules.NumberOfCards; i++)
        {
            var card = new Card(Guid.NewGuid(), guidMatches.Dequeue());
            cards.Add(card);
        }

        Shuffle(cards);

        Cards = cards;
    }

    // Based on Fisher–Yates shuffle:
    // http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
    private void Shuffle(List<Card> cards)
    {
        var r = new Random();
        for (int n = cards.Count - 1; n > 0; --n)
        {
            int k = r.Next(n + 1);
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }
    }
}
