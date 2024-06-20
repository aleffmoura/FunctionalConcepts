namespace BookApi.Api.ModuleAutoFac;

using System.Reflection;
using Autofac;
using BookApi.Api.Behaviors;
using BookApi.ApplicationService;
using MediatR;
using Module = Autofac.Module;

public class MediatRModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = typeof(ApplicationAssemblyClass).GetTypeInfo().Assembly;

        builder.RegisterAssemblyTypes(assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(assembly)
               .AsClosedTypesOf(typeof(IRequestHandler<>))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(assembly)
               .AsClosedTypesOf(typeof(INotificationHandler<>))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(ValidatorBehavior<,>))
               .As(typeof(IPipelineBehavior<,>));
    }
}
