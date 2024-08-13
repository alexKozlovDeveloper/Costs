using CostKeeper.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CostKeeper.Api.Handlers
{
	[ApiController]
	[Route("v1/checks")]
	public class CreateCheckController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

		[HttpPost]
		public async Task<ActionResult<IEnumerable<Check>>> Handle(
			[FromBody] CreateCheckCommand command
			)
		{
			var items = await _mediator.Send(command);
			return Ok(items);
		}
	}

	public record CreateCheckCommand(
		string ProductId,
		DateTime Date,
		float Price,
		float Count
		) : IRequest<Check>;

	public class CreateCheckHandler(CostsDbContext dbContext) : IRequestHandler<CreateCheckCommand, Check>
	{
		private readonly CostsDbContext _dbContext = dbContext;

		public async Task<Check> Handle(CreateCheckCommand request, CancellationToken ct)
		{
			var check = new Check
			{
				ProductId = request.ProductId,
				Date = request.Date.ToUniversalTime(),
				Price = request.Price,
				Count = request.Count
			};

			await _dbContext.Checks.AddAsync(check, ct);
			await _dbContext.SaveChangesAsync(ct);

			return check;
		}
	}
}
