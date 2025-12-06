using MemoryMatchingGame.Core.Services.Implementations;
using MemoryMatchingGame.Core.Services.Implementations.Rules;
using MemoryMatchingGame.Core.Services.Interfaces;
using MemoryMatchingGame.Core.Services.Interfaces.Rules;
using MemoryMatchingGame.Infrastructure.Services.Implementations;
using MemoryMatchingGame.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MemoryMatchingGame.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IMatchChecker, MatchChecker>();
        services.AddSingleton<IShuffler, Shuffler>();
        services.AddSingleton<IRuleSetConstraints, RuleSetConstraints>();
        services.AddSingleton<IRuleSetProvider, RuleSetProvider>();

        services.AddSingleton<IGameEngine, GameEngine>();   
        services.AddSingleton<IGameContext, GameContext>();

        return services;
    }
}
