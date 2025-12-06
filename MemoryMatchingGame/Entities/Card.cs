using MemoryMatchingGame.Core.Enums;

namespace MemoryMatchingGame.Core.Entities;

public class Card(Guid id, Guid matchingKey)
{
    public Guid Id { get; } = id;
    public Guid MatchingKey { get; } = matchingKey;
    public CardStatus Status { get; set; } = CardStatus.NonFlipped;
}
