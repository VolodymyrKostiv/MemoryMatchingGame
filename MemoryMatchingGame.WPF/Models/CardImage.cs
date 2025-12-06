using MemoryMatchingGame.Core.Entities;

namespace MemoryMatchingGame.WPF.Models;

public class CardImage
{
    public Card? Card { get; set; }
    public string ImagePath { get; set; } = string.Empty;
}
