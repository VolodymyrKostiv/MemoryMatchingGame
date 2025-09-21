using MemoryMatchingGame.Core.Enums;
using MemoryMatchingGame.Core.Models;
using MemoryMatchingGame.Core.Services.Interfaces;

namespace MemoryMatchingGame.Core.Services.Implementations;

internal class GameEngine : IGameEngine
{
    private readonly IMatchChecker _matchChecker;
    private readonly GameRules _gameRules;
    private readonly GameBoard _board;

    public GameEngine(IMatchChecker matchChecker, GameRules gameRules)
    {
        _matchChecker = matchChecker;
        _gameRules = gameRules;
    }

    public void StartNewGame(GameRules ruleSet)
    {
        var board = new GameBoard();
        board.Initialize(ruleSet);
    }

    public void FlipCard(Card card)
    {
        card.Status = CardStatus.Flipped;
    }

    public void CheckForMatch()
    {
        var checkResult = _matchChecker.Check(_board.FlippedCards, _gameRules);
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
        return _board.Cards.All(card => card.Status == CardStatus.Matched);
    }

    private void MatchFlippedCards()
    {
        _board.FlippedCards.ForEach(card => card.Status = CardStatus.Matched);
    }

    private void ReturnFlippedCardsToNonFlipped()
    {
        _board.FlippedCards.ForEach(card => card.Status = CardStatus.NonFlipped);
    }
}
