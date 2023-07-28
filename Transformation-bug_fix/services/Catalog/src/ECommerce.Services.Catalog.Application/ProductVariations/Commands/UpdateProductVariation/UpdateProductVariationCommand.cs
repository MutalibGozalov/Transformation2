namespace ECommerce.Services.Catalog.Application.ProductVariations.Commands.UpdateProductVariation;
public class UpdateProductVariationCommand : IRequest<Response<NoContent>>
{
    public string  Id { get; set; } = null!;
}

public class UpdateProductVariationCommandHandler : IRequestHandler<UpdateProductVariationCommand, Response<NoContent>>
{
    private readonly IMongoCollection<ProductVariation> _productVariationCollection;
    private readonly IMapper _mapper;

    public UpdateProductVariationCommandHandler(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._productVariationCollection = database.GetCollection<ProductVariation>(databaseSettings.ProductVariationCollectionName);

        _mapper = mapper;
    }
    public async Task<Response<NoContent>> Handle(UpdateProductVariationCommand request, CancellationToken cancellationToken)
    {
        var productVariation = _mapper.Map<ProductVariation>(request);
        productVariation.ModifiedAt = DateTime.UtcNow;
        var result = await _productVariationCollection.FindOneAndReplaceAsync(p => p.Id == request.Id, productVariation);

        if (result is null)
        {
            return Response<NoContent>.Failure("Product not found", 404);
        }

        return Response<NoContent>.Success(200);
    }
}
