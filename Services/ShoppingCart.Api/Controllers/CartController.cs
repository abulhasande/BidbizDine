using AutoMapper;
using Dine.MessageBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ShoppingCart.Api.Data;
using ShoppingCart.Api.Models;
using ShoppingCart.Api.Models.Dto;
using ShoppingCart.Api.Service;

namespace ShoppingCart.Api.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ResponseDto _responseDto;
        private IMapper _mapper;
        private readonly ShoppingCartDbContext _dbContext;
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;
        private readonly IMessageBus _messageBus;
        private readonly IConfiguration _configuration; 
        public CartController(IMapper mapper, ShoppingCartDbContext dbContext, 
            IProductService productService, 
            ICouponService couponService,
            IMessageBus messageBus, IConfiguration configuration)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _responseDto = new ResponseDto();
            _productService = productService;
            _couponService = couponService;
            _messageBus = messageBus;
            _configuration = configuration;
        }


        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCart(string userId)
        
        {
            try
            {
                var cart = new CartDto();
                cart.CartHeader = _mapper.Map<CartHeaderDto>(_dbContext.CartHeaders.First(x => x.UserId == userId));
                cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(_dbContext.CartDetails
                                          .Where(x => x.CartHeaderId == cart.CartHeader.CartHeaderId));

                IEnumerable<ProductDto> productDtos =await  _productService.GetProducts();

                foreach(var item in cart.CartDetails)
                {
                    item.Product = productDtos.FirstOrDefault(u => u.ProductId == item.ProductId);
                    cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
                }
                //apply Coupon if have
                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    CooponDto coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);
                    if (coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= coupon.DiscountAmount;
                        cart.CartHeader.Discount = coupon.DiscountAmount;
                    }

                }
                _responseDto.Result = cart;
            }
            catch (Exception ex)
            {

                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }

            return _responseDto;
        }


        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                var cartFromDb =await _dbContext.CartHeaders.FirstAsync(u => u.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = cartDto.CartHeader.CouponCode;
                _dbContext.Update(cartFromDb);

                await _dbContext.SaveChangesAsync();

                _responseDto.Result = true;
            }
            catch (Exception ex)
            {

                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }

            return _responseDto;
        }

        [HttpPost("EmailCartRequest")]
        public async Task<object> EmailCartRequest([FromBody] CartDto cartDto)
        {
            try
            {
                await _messageBus.PublishMessage(cartDto, _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCart"));

                _responseDto.Result = true;
            }
            catch (Exception ex)
            {

                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }

            return _responseDto;
        }


        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _dbContext.CartHeaders.AsNoTracking()
                                      .FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    //cerate header and details
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _dbContext.CartHeaders.Add(cartHeader);
                    await _dbContext.SaveChangesAsync();

                    cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    _dbContext.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _dbContext.SaveChangesAsync();
                    
                }
                else
                {
                    // if Heder is null 
                    //check cart has same item

                    var cartDetailsFromDb = await _dbContext.CartDetails.AsNoTracking().FirstOrDefaultAsync( u => u.ProductId == cartDto.CartDetails.First().ProductId 
                                                                                                && u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                    if(cartDetailsFromDb == null)
                    {
                        //create cartDetails
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        _dbContext.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        //Update Count
                        cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                        _dbContext.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _dbContext.SaveChangesAsync();
                    }
                }

                _responseDto.Result = cartDto;
            }
            catch (Exception ex)
            {

                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }

            return _responseDto;
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                var cartDetials =  _dbContext.CartDetails.First(x => x.CartDetailsId == cartDetailsId);

                var totalCountofCartItem = _dbContext.CartDetails.Where(x => x.CartHeaderId == cartDetials.CartHeaderId).Count();

                _dbContext.CartDetails.Remove(cartDetials);

                if(totalCountofCartItem == 1)
                {
                    var cartHeaderToRemove = await _dbContext.CartHeaders.FirstOrDefaultAsync(u => u.CartHeaderId == cartDetials.CartHeaderId);

                    _dbContext.CartHeaders.Remove(cartHeaderToRemove);
                }

                await _dbContext.SaveChangesAsync();

                _responseDto.Result = true;
            }
            catch (Exception ex)
            {

                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }

            return _responseDto;
        }
    }
}
