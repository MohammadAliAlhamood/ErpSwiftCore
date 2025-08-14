using AutoMapper;
using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.CommandsHandler
{
    public class CreateProductCommandHandler : BaseHandler<CreateProductCommand>
    {
        private readonly IProductCommandService _svc;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(
            IProductCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<CreateProductCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(CreateProductCommand req, CancellationToken ct)
        {
            var entity = _mapper.Map<Domain.Entities.EntityProduct.Product>(req.Product);
            var id = await _svc.CreateProductAsync(entity, ct);
            return new { Id = id };
        }
    }



} 