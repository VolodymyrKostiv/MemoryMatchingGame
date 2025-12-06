using MemoryMatchingGame.Core.Entities;
using MemoryMatchingGame.Core.Enums;
using MemoryMatchingGame.Core.Services.Interfaces;
using MemoryMatchingGame.Core.Services.Interfaces.Rules;
using System.Collections.ObjectModel;

namespace MemoryMatchingGame.Core.Services.Implementations;

public sealed class GameEngine : IGameEngine
{
    private readonly IMatchChecker _matchChecker;
    private readonly IShuffler _shuffler;
    private IRuleSet _ruleSet;

    public ObservableCollection<Card>? Cards { get; private set; }

    public ObservableCollection<Card>? FlippedCards => Cards == null ? null : 
        [.. Cards.Where(c => c.Status == CardStatus.Flipped)];

    public GameEngine(IMatchChecker matchChecker, IShuffler shuffler)
    {
        _matchChecker = matchChecker;
        _shuffler = shuffler;
    }

    public void StartNewGame(IRuleSet ruleSet)
    {
        _ruleSet = ruleSet;
        InitializeCards(_ruleSet);
    }

    public void InitializeCards(IRuleSet gameRules)
    {
        int numberOfMatches = gameRules.TotalCards / gameRules.CardsPerMatch;
        var guidMatches = new List<Guid>(numberOfMatches);
        for (int i = 0; i < numberOfMatches; i++)
        {
            var guid = Guid.NewGuid();
            guidMatches.AddRange(Enumerable.Repeat(guid, gameRules.CardsPerMatch));
        }

        var cards = guidMatches.Select(guid => new Card(Guid.NewGuid(), guid)).ToList();

        _shuffler.FisherYatesShuffle(cards);

        Cards = new ObservableCollection<Card>(cards);
    }

    public (int Rows, int Cols) CalculateGrid(int totalCards)
    {
        int cols = (int)Math.Ceiling(Math.Sqrt(totalCards));
        int rows = (int)Math.Ceiling((double)totalCards / cols);

        return (rows, cols);
    }

    public void FlipCard(Card card)
    {
        card.Status = CardStatus.Flipped;
    }

    public void CheckForMatch()
    {
        if (FlippedCards == null)
        {
            return;
        }

        var checkResult = _matchChecker.Check(FlippedCards, _ruleSet);
        if (checkResult == MatchStatus.NotMatch)
        {
            ReturnFlippedCardsToNonFlipped();
        }
        else if (checkResult == MatchStatus.AllMatched)
        {
            MatchFlippedCards();
        }
    }

    public bool IsGameFinished()
    {
        return Cards != null && Cards.All(card => card.Status == CardStatus.Matched);
    }

    private void MatchFlippedCards()
    {
        foreach (var card in FlippedCards)
        {
            card.Status = CardStatus.Matched;
        }
    }

    private void ReturnFlippedCardsToNonFlipped()
    {
        foreach (var card in FlippedCards)
        {
            card.Status = CardStatus.NonFlipped;
        }
    }
}
