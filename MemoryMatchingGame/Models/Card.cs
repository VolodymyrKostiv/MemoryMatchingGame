using MemoryMatchingGame.Core.Enums;

namespace MemoryMatchingGame.Core.Models;

public class Card(Guid id, Guid matchingKey)
{
    public Guid Id { get; set; } = id;
    public Guid MatchingKey { get; set; } = matchingKey;
    public CardStatus Status { get; set; } = CardStatus.NonFlipped;
    public string ImageKey { get; set; } = string.Empty;
}
