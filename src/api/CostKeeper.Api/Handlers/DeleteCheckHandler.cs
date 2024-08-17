using CostKeeper.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CostKeeper.Api.Handlers
{
	[ApiController]
	[Route("v1/checks")]
	public class DeleteCheckController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

		[HttpDelete("{checkId}")]
		public async Task<ActionResult<IEnumerable<Check>>> Handle(
			[FromRoute] int checkId
			)
		{
			var items = await _mediator.Send(new DeleteCheckCommand(checkId));
			return Ok(items);
		}
	}

	public record DeleteCheckCommand(
		int CheckId
		) : IRequest<Check>;

	public class DeleteCheckHandler(CostsDbContext dbContext) : IRequestHandler<DeleteCheckCommand, Check>
	{
		private readonly CostsDbContext _dbContext = dbContext;

		public async Task<Check> Handle(DeleteCheckCommand request, CancellationToken ct)
		{
			var check = await _dbContext.Checks
				.FirstAsync(a => a.Id == request.CheckId, ct);

			_dbContext.Checks.Remove(check);

			await _dbContext.SaveChangesAsync(ct);

			return check;
		}
	}
}
