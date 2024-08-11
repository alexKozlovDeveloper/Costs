using CostKeeper.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CostKeeper.Api.Handlers
{
	[ApiController]
	[Route("v1/products")]
	public class ListProductsController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> Handle(
			[FromQuery] string? query
			)
		{
			var items = await _mediator.Send(new ListProductsQuery(query));
			return Ok(items);
		}
	}

	public record ListProductsQuery(
		string? Query
		) : IRequest<IEnumerable<Product>>;

	public class ListProductsHandler(CostsDbContext dbContext) : IRequestHandler<ListProductsQuery, IEnumerable<Product>>
	{
		private readonly CostsDbContext _dbContext = dbContext;

		public async Task<IEnumerable<Product>> Handle(ListProductsQuery request, CancellationToken ct)
		{
			var query = _dbContext.Products
				.AsQueryable()
				.AsNoTracking();

			if(!string.IsNullOrEmpty(request.Query))
			{
				query = query.Where(a => a.Id.ToLower().Contains(request.Query.ToLower()));
			}

			return await query.ToListAsync(ct);
		}
	}
}
