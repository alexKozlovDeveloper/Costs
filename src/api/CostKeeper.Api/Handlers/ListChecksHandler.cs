using CostKeeper.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CostKeeper.Api.Handlers
{
	[ApiController]
	[Route("v1/checks")]
	public class ListChecksController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Check>>> Handle()
		{
			var items = await _mediator.Send(new ListChecksQuery());
			return Ok(items);
		}
	}

	public record ListChecksQuery() : IRequest<IEnumerable<Check>>;

	public class ListChecksHandler(CostsDbContext dbContext) : IRequestHandler<ListChecksQuery, IEnumerable<Check>>
	{
		private readonly CostsDbContext _dbContext = dbContext;

		public async Task<IEnumerable<Check>> Handle(ListChecksQuery request, CancellationToken ct)
		{
			var items = await _dbContext.Checks.ToListAsync(ct);

			return items;
		}
	}
}
