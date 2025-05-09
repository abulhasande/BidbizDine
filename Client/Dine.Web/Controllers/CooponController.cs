using Dine.Web.Models;
using Dine.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dine.Web.Controllers
{
    public class CooponController : Controller
    {
        private readonly ICooponService _cooponService;

        public CooponController(ICooponService cooponSercie)
        {
            _cooponService = cooponSercie;
        }

        public async Task<IActionResult> CooponIndex()
        {
            List<CooponDto>? list = new();
            ResponseDto? response = await _cooponService.GetAllCooponAsync();
            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CooponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message; 
            }

            return View(list);
        }

        public async Task<IActionResult> CreateCoopon()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoopon(CooponDto model)
        {
            if(ModelState.IsValid)
            {
                ResponseDto? response = await _cooponService.CreateCooponAsync(model);

                if(response != null && response.IsSuccess)
                {
                    TempData["success"] = "Successfully created";
                    return RedirectToAction(nameof(CooponIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> GetByCooponCode(string code)
        {
        
            ResponseDto? response = await _cooponService.GetByCooponCodeAsync(code);
            CooponDto coponDto = new CooponDto();
            if (response != null && response.IsSuccess)
            {
                coponDto = JsonConvert.DeserializeObject<CooponDto>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(coponDto);
        }

        public async Task<IActionResult> DeleteCoopon(int couponId)
        {

            ResponseDto? response = await _cooponService.GetCooponByIdAsync(couponId);
         
            if (response != null && response.IsSuccess)
            {
                CooponDto? coponDto = JsonConvert.DeserializeObject<CooponDto>(Convert.ToString(response.Result));
                return View(coponDto);
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCoopon(CooponDto cooponDto)
        {

            ResponseDto? response = await _cooponService.DeleteCooponAsync(cooponDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Successfully Deleted";
                return RedirectToAction(nameof(CooponIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(cooponDto);
        }
    }
}
