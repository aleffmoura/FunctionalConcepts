namespace BookApi.Api.ModuleAutoFac;

using Autofac;
using BookApi.ApplicationService;
using FluentValidation;

public class FluentValidationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(ApplicationAssemblyClass).Assembly)
            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
