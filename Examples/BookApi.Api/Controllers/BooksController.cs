namespace BookApi.Api.Controllers;

using BookApi.Api.Bases;
using BookApi.Api.Dtos;
using BookApi.ApplicationService.Commands;
using BookApi.ApplicationService.Notifications;
using BookApi.ApplicationService.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BooksController : ApiControllerBase<BooksController>
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator, ILogger<BooksController> logger)
        : base(logger)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookDto dto)
    {
        var result = await _mediator.Send(new BookCreateCommand
        {
            Title = dto.Title,
            Author = dto.Author,
            BookCoverUrl = dto.BookCoverUrl,
            Released = dto.Released,
        });

        return HandleCommand(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Read([FromRoute] Guid id)
        => HandleQuery(await _mediator.Send(new BookByIdQuery { Id = id }));

    [HttpGet]
    public async Task<IActionResult> ReadAll()
        => HandleQueryable(await _mediator.Send(new BookCollectionQuery()));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] BookDto dto)
        => HandleCommand(await _mediator.Send(new BookUpdateCommand
        {
            Id = id,
            Title = dto.Title,
            Author = dto.Author,
            Released = dto.Released,
        }));

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCoverUrl([FromRoute] Guid id, [FromBody] PatchBookDto dto)
    {
        return await HandleAccepted(
            new BookPatchNotification
            {
                BookId = id,
                Url = dto.Url,
            },
            CancellationToken.None,
            _mediator.Publish);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
        => HandleCommand(await _mediator.Send(new BookRemoveCommand(id)));



}
