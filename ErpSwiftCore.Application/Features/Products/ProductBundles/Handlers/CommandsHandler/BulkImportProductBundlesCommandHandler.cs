using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.CommandsHandler
{
    public class BulkImportProductBundlesCommandHandler : BaseHandler<BulkImportProductBundlesCommand>
    {
        private readonly IProductBundleCommandService _commandService;
        private readonly IMapper _mapper;
        public BulkImportProductBundlesCommandHandler(
            IProductBundleCommandService commandService,
            IMapper mapper,
            ILogger<BulkImportProductBundlesCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(BulkImportProductBundlesCommand request, CancellationToken cancellationToken)
        {
            var entities = request.Bundles.Select(dto => _mapper.Map<ProductBundle>(dto));
            var count = await _commandService.BulkImportBundlesAsync(entities, cancellationToken);
            var resultDto = new ProductBundleBulkImportResultDto
            {
                ImportedCount = count
            };
            return resultDto;
        }
    }


}
