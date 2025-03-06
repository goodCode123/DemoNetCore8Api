using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repository.Model;
using Services.InterFace;

namespace DemoNetCore8Api.Service.Attributes
{

    public class AuthorizePermissionAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _requiredPermission;

        public AuthorizePermissionAttribute(string requiredPermission)
        {
            _requiredPermission = requiredPermission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // 假設從 JWT Token 或 HttpContext 取得用戶權限
            var userPermissions = context.HttpContext.User.FindFirst("Id")?.Value;
            var layOutService = context.HttpContext.RequestServices.GetService(typeof(ILayOutService)) as ILayOutService;
            List<MenuItemEntity> result = layOutService.GetSideBarMeauListAuthorize(userPermissions);

            var auth =  result.Where(x=>x.Url == _requiredPermission);

            if (!auth.Any())
            {
                context.Result = new ForbidResult(); // 無權限時返回 403
            }
        }
    }
}
