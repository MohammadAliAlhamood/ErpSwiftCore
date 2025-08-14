using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{
    // Soft Delete single
    public class SoftDeleteProductBundleCommand :
        IRequest<APIResponseDto>
    {
        public Guid BundleId { get; }
        public SoftDeleteProductBundleCommand(Guid bundleId)
        {
            BundleId = bundleId;
        }
    }


}
