namespace BookApi.Api.Bases;

using FluentValidation;
using FunctionalConcepts.Enums;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class ApiControllerBase<TController> : ControllerBase
    where TController : ApiControllerBase<TController>
{
    private ILogger<TController> _logger;

    public ApiControllerBase(ILogger<TController> logger)
    {
        _logger = logger;
    }

    public async ValueTask<IActionResult> HandleAccepted<TIn>(
        TIn notification,
        CancellationToken token,
        Func<TIn, CancellationToken, Task> publish)
        where TIn : INotification
    {
        try
        {
            _ = Task.Run(async () => await publish(notification, token));
            return await Task.FromResult(Accepted());
        }
        catch (Exception ex)
        {
            return HandleFailure((UnhandledError)("Erro ao realizar handle accepted", ex));
        }
    }

    public IActionResult HandleCommand<TOut>(Result<TOut> result)
        => result.Match(value => Ok(value), HandleFailure);

    public IActionResult HandleQuery<TOut>(Result<TOut> result)
        => result.Match(value => Ok(value), HandleFailure);

    public IActionResult HandleQueryable<TSource>(Result<IQueryable<TSource>> result)
        => result.Match(succ => Ok(succ.ToList()), HandleFailure);

    private IActionResult HandleFailure(BaseError erro)
    {
        _logger.LogError("Ocorreram erros na consulta do livro, errorCode: {code}, mensagem: {msg}, exception: {exn}", erro.Code, erro.Message, erro.Exception);

        return erro.Exception switch
        {
            null => ResolveError(erro, "Error"),
            ValidationException exn => StatusCode(erro.Code, exn.Errors),
            _ => ResolveError(erro, "Exception"),
        };

        IActionResult ResolveError(BaseError erro, string title)
            => Problem(statusCode: erro.Code, title: title, detail: erro.Message, type: $"{(EErrorCode)erro.Code}");
    }
}
