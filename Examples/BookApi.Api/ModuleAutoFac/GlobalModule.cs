namespace BookApi.Api.ModuleAutoFac;

using Autofac;
using BookApi.Domain.Bases;
using BookApi.Domain.Features.Books;
using BookApi.Domain.Interfaces.Books;
using BookApi.Infra.Data.Contexts;
using BookApi.Infra.Data.Features.Books;
using Microsoft.EntityFrameworkCore;

public class GlobalModule : Autofac.Module
{
    private IConfigurationRoot _configuration;
    public GlobalModule(IConfigurationRoot configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(Logger<>))
               .As(typeof(ILogger<>))
               .SingleInstance();

        builder.RegisterType<BookRepository>()
               .As<IBookRepository>()
               .InstancePerLifetimeScope();

        builder.RegisterType<BookReadRepository>()
               .As<IReadRepository<Book>>()
               .InstancePerLifetimeScope();

        builder.Register(_ => _configuration)
               .As<IConfigurationRoot>()
               .InstancePerLifetimeScope();

        builder.Register(ctx =>
        {
            var opt = new DbContextOptionsBuilder<BookstoreContext>()
                                             .UseSqlite("Data Source=app.db")
                                             .Options;
            return new BookstoreContext(opt);
        }).AsSelf()
        .InstancePerLifetimeScope();
    }
}
