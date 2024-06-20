namespace BookApi.Api.Extensions;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using BookApi.Api.ModuleAutoFac;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;

public static class AutofacExt
{
    public static IHostBuilder ConfigureAutofac(this IHostBuilder hostBuilder, IConfigurationRoot cfgRoot)
    {
        return hostBuilder
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
            {
                var configuration = MediatRConfigurationBuilder
                                    .Create(typeof(Program).Assembly)
                                    .WithAllOpenGenericHandlerTypesRegistered()
                                    .WithRegistrationScope(RegistrationScope.Scoped)
                                    .Build();

                containerBuilder.RegisterModule(new FluentValidationModule());
                containerBuilder.RegisterModule(new GlobalModule(cfgRoot));
                containerBuilder.RegisterModule(new MediatRModule());

                containerBuilder.RegisterMediatR(configuration);
                containerBuilder.Register(r => containerBuilder).AsSelf().InstancePerLifetimeScope();
            })
            .ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(60));
    }
}
