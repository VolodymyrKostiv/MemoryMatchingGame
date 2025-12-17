using MemoryMatchingGame.Core.Entities;
using System.Windows.Media.Imaging;

namespace MemoryMatchingGame.WPF.Models;

public class CardImage
{
    public Card? Card { get; set; }
    public BitmapImage? Image { get; set; }
}
