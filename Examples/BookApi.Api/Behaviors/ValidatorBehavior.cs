namespace BookApi.Api.Behaviors;

using FluentValidation;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;

public class ValidatorBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : notnull
{
    private readonly IValidator<TRequest>[] _validators;

    public ValidatorBehavior(IValidator<TRequest>[] validators) => _validators = validators;

    public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
    {
        List<FluentValidation.Results.ValidationFailure> failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        return failures.Any()
               ? (InvalidObjectError)("Erro ao executar validations", new ValidationException(failures))
               : await next();
    }
}
