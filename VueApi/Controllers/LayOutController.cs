using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Model;
using Service.Model;
using Services.InterFace;

namespace DemoNetCore8Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class LayOutController : ControllerBase
    {
        private readonly ILayOutService layOut;

        public LayOutController(ILayOutService _layOut)
        {
            layOut = _layOut;
        }

        [HttpGet]
        public ApiResponseModel GetSideBarMeauList(string Id)
        {
            return layOut.GetSideBarMeauList(Id);
        }

        [HttpGet]
        public ApiResponseModel GeMeauList()
        {
            return layOut.GetMenus();
        }

        [HttpPost]
        public ApiResponseModel UpdateMeauList(MenuItemEntity menuItem)
        {
            return layOut.UpdateMeauList(menuItem);
        }

        [HttpPost]
        public ApiResponseModel AddMeauList(MenuItemEntity menuItem)
        {
            return layOut.AddMeauList(menuItem);
        }

        [HttpPost]
        public ApiResponseModel GetNoDisableMeaus()
        {
           return layOut.GetNoDisableMeaus();
        }
    }
}
