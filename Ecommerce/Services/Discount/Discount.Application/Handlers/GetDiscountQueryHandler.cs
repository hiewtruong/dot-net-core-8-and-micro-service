using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        public IDiscountRepository _discountRepository { get; set; }
        private readonly ILogger<GetDiscountQueryHandler> _logger;
        public GetDiscountQueryHandler(IDiscountRepository discountRepository, ILogger<GetDiscountQueryHandler> logger)
        {
            _discountRepository = discountRepository;
            _logger = logger;
        }

        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product name {request.ProductName} not found."));
            }
            var couponModel = new CouponModel
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount
            };
            _logger.LogInformation($"Coupon for product {coupon.ProductName} is fetched.");
            return couponModel;
        }
    }
}
