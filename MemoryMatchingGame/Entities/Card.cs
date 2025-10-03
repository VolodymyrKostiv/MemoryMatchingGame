using MemoryMatchingGame.Core.Enums;

namespace MemoryMatchingGame.Core.Entities;

public class Card
{
    public Guid Id { get; private set; }
    public Guid MatchingKey { get; private set; }
    public CardStatus Status { get; set; } = CardStatus.NonFlipped;
    public string ImageKey { get; set; } = string.Empty;

    public Card(Guid id, Guid matchingKey)
    {
        Id = id;
        MatchingKey = matchingKey;
    }
}
