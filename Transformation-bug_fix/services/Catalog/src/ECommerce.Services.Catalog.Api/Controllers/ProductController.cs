using ECommerce.Services.Catalog.Application.Categories.Commands.CreateCategory;
using ECommerce.Services.Catalog.Application.Categories.Queries;
using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.Services.Catalog.Api.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ProductController : CustomBaseController
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    // private IMediator _mediator;
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await Mediator.Send(new GetCategoriesQuery());
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await Mediator.Send(new GetCategoryByIdQuery(id));
        return CreateActionResultInstance(response);
    }

    // [HttpGet("{storeId}")]
    // public async Task<IActionResult> GetByStoreId(int storeId)
    // {
    //     var response = await _productService.GetAllByStoreIdAsync(storeId);
    //     return CreateActionResultInstance(response);
    // }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand categoryCommand)
    {
        var response = await Mediator.Send(categoryCommand);
        return CreateActionResultInstance(response);
    }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto, string id)
    // {
    //     var response = await _productService.UpdateAsync(productUpdateDto, id);
    //     return CreateActionResultInstance(response);
    // }

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(string id)
    // {
    //     var response = await _productService.DeleteAsync(id);
    //     return CreateActionResultInstance(response);
    // }

}