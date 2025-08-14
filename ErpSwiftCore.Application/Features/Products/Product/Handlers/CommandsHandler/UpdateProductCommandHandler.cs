using AutoMapper;
using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.CommandsHandler
{

    public class UpdateProductCommandHandler : BaseHandler<UpdateProductCommand>
    {
        private readonly IProductCommandService _svc;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(   IProductCommandService svc,     IMapper mapper,   ILogger<BaseHandler<UpdateProductCommand>> logger  ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        } 
        protected override async Task<object?> HandleRequestAsync(UpdateProductCommand req, CancellationToken ct)
        {
            var entity = _mapper.Map<Domain.Entities.EntityProduct.Product>(req.Product);
            var ok = await _svc.UpdateProductAsync(entity, ct);
            return new { Success = ok };
        }
    } 
}
