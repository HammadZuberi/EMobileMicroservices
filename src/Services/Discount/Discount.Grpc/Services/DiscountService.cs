using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService
        (DiscountContext _context, ILogger<DiscountService> _logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {


            //TODO:Get discount from DB
            var coupon = await _context.Coupons
                .FirstOrDefaultAsync(x => x.ProductName.ToLower() == request.ProductName.ToLower());

            if (coupon == null)
                coupon = new Coupon { ProductName = "No Discount", Amount = 0 };

            _logger.LogInformation("Discount retrieved Product {name} ,Amount {amount}, ", coupon.ProductName, coupon.Amount);

            var couponModel = new CouponModel()
            { Amount = coupon.Amount, ProductName = coupon.ProductName, Description = coupon.Description, Id = coupon.Id };
            return couponModel;

        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));


            _logger.LogInformation("Discount is created succussfully  Product {name} ,Amount {amount}, ", coupon.ProductName, coupon.Amount);
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();


            var couponmodel = coupon.Adapt<CouponModel>();
            return couponmodel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

            _logger.LogInformation("Discount is succussfully updated from prev Product {prevProd}  Product {name} ,Amount {amount}, "
                , request.Coupon.ProductName, coupon.ProductName, coupon.Amount);

            _context.Coupons.Update(coupon);
            await _context.SaveChangesAsync();


            var couponmodel = coupon.Adapt<CouponModel>();
            return couponmodel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _context.Coupons
                 .FirstOrDefaultAsync(x => x.ProductName.ToLower() == request.ProductName.ToLower());

            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Product Name={request.ProductName} not found."));


            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Discount is succussfully deleted Product {name}", coupon.ProductName);

            return new DeleteDiscountResponse() { Success = true };
        }
    }

}
