namespace BookApi.ApplicationService.Notifications.Handlers;

using System.Text;
using BookApi.ApplicationService.Notifications;
using BookApi.Domain.Interfaces.Books;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class BookPatchNotificationHandler : INotificationHandler<BookPatchNotification>
{
    private readonly IServiceProvider _provider;

    public BookPatchNotificationHandler(IServiceProvider provider)
    {
        _provider = provider;
    }

    async Task INotificationHandler<BookPatchNotification>.Handle(BookPatchNotification notification, CancellationToken cancellationToken)
    {
        var scoped = _provider.CreateScope();
        var logger = scoped.ServiceProvider.GetService<ILogger<BookPatchNotificationHandler>>();

        try
        {
            var bookRepository = scoped.ServiceProvider.GetService<IBookRepository>() ?? throw new Exception("BookRepository não instanciado");

            var maybeBook = await bookRepository.GetById(notification.BookId);

            var result = await maybeBook.MatchAsync(
                                        async book =>
                                        {
                                            book.BookCoverUrl = notification.Url;

                                            return Result.Of(await bookRepository.Update(book, cancellationToken));
                                        },
                                        () => (NotFoundError)$"Book with id: {notification.BookId} not found");

            result.Else(fail => logger?.LogCritical(fail.Message));
        }
        catch (Exception ex)
        {
            logger?.LogCritical("Error while sending notification, message: {msg}, exception: {ex}", ex.Message, ex);
        }
    }
}
