using CostKeeper.Api.Models;
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
		public async Task<ActionResult<IEnumerable<ProductDto>>> Handle(
			[FromQuery] string? query
			)
		{
			var items = await _mediator.Send(new ListProductsQuery(query));
			return Ok(items);
		}
	}

	public record ListProductsQuery(
		string? Query
		) : IRequest<IEnumerable<ProductDto>>;

	public class ListProductsHandler(CostsDbContext dbContext) : IRequestHandler<ListProductsQuery, IEnumerable<ProductDto>>
	{
		private readonly CostsDbContext _dbContext = dbContext;

		public async Task<IEnumerable<ProductDto>> Handle(ListProductsQuery request, CancellationToken ct)
		{
			var query = _dbContext.Products
				.AsQueryable()
				.AsNoTracking();

			if(!string.IsNullOrEmpty(request.Query))
			{
				query = query.Where(a => a.Id.ToLower().Contains(request.Query.ToLower()));
			}

			return await query
				.Select(a => new ProductDto 
				{
					Id = a.Id,
					Description = a.Description,
					Category = a.Category,
					Tags = a.Tags,
					Weight = a.Weight,
					EnergyValue = a.EnergyValue,
					Proteins = a.Proteins,
					Fats = a.Fats,
					Carbohydrates = a.Carbohydrates,
					ProductUnitEnergyValue = a.EnergyValue * a.Weight * 0.01f
				})
				.ToListAsync(ct);
		}
	}
}
