namespace BookApi.ApplicationService.Notifications;

using System;
using FluentValidation;
using MediatR;
public class BookPatchNotification : INotification
{
    public Guid BookId { get; set; }
    public string Url { get; set; } = string.Empty;

    private class BookPatchCommandValidator : AbstractValidator<BookPatchNotification>
    {
        public BookPatchCommandValidator()
        {
            RuleFor(a => a.Url)
                    .NotEmpty()
                    .WithMessage("Data cant be null or empty");
        }
    }
}
