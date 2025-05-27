using Dine.Web.Models;
using Dine.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Dine.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBaseOnLoggedInUser());
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            return View(await LoadCartDtoBaseOnLoggedInUser());
        }

        private async Task<CartDto> LoadCartDtoBaseOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? responseDto =  await _shoppingCartService.GetCartByUserIdAsync(userId);
            if(responseDto != null && responseDto.IsSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(responseDto.Result));
                return cartDto;
            }

            return new CartDto(); 
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? responseDto = await _shoppingCartService.RemoveFromCartAsync(cartDetailsId);

            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {

            ResponseDto? responseDto = await _shoppingCartService.ApplyCooponAsync(cartDto);

            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = "Coupon Applied successfully";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmailCart(CartDto cartDto)
        {
            CartDto cart = await LoadCartDtoBaseOnLoggedInUser();
            cart.CartHeader.Email = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Email)?.FirstOrDefault()?.Value;
            ResponseDto? responseDto = await _shoppingCartService.EmailCart(cartDto);

            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = "Email Will be processed and sent shortly";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            cartDto.CartHeader.CouponCode = string.Empty; 

            ResponseDto? responseDto = await _shoppingCartService.ApplyCooponAsync(cartDto);

            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = "Coupon removed successfully";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        //private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        //{
        //    var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
        //    ResponseDto? response = await _shoppingCartService.GetCartByUserIdAsync(userId);
        //    if(response != null && response.IsSuccess)
        //    {
        //        CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
        //        return cartDto;
        //    }

        //    return new CartDto();
        //}
    }
}
