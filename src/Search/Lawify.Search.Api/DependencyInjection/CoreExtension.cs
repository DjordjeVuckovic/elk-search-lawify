using System.Reflection;
using Elastic.Clients.Elasticsearch;
using Lawify.Common.Mediator;
using Lawify.Search.Api.Features.SerbianSearch.Contracts;
using Lawify.Search.Api.Features.SerbianSearch.Laws;
using Lawify.Search.Api.Features.SerbianSearch.Shared.Shared;
using MediatR;

namespace Lawify.Search.Api.DependencyInjection;

public static class CoreExtension
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddTransient(typeof(IRequestHandler<Highlighter.Query<SerbianLawIndex>, SearchResponse<SerbianLawIndex>>), typeof(Highlighter.Handler<SerbianLawIndex>));
        services.AddTransient(typeof(IRequestHandler<Highlighter.Query<SerbianContractIndex>, SearchResponse<SerbianContractIndex>>), typeof(Highlighter.Handler<SerbianContractIndex>));
        return services;
    }
}