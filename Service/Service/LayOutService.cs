using Infrastructure.Attributes;
using Repository.InterFace;
using Repository.Model;
using Service.Model;
using Services.EnumsModel;
using Services.InterFace;

namespace Services.Service
{
    [PerLifetimeScopeService]
    public class LayOutService : ILayOutService
    {
        private readonly ILayOutDal layOutDal;
        public LayOutService(ILayOutDal _layOutDal) {
            layOutDal = _layOutDal;
        }

        public ApiResponseModel GetSideBarMeauList(string Id)
        {           
            var meauResluts =  layOutDal.GetSideBarMeauList(Id);
            var sideBars =  ConvertToMenuItems(meauResluts);
            string jsonString = @"
        [
            {
                ""title"": ""範例程式"",
                ""url"": ""1"",
                ""icon"": ""Setting"",
                ""children"": [
                    {
                        ""title"": ""批次更新資料範例"",
                        ""icon"": ""Setting"",
                        ""url"": ""/BatchUploadExample""
                    },
                    {
                        ""title"": ""新增刪除修改範例"",
                        ""icon"": ""Setting"",
                        ""url"": ""/CrudExample""
                    },
                    {
                        ""title"": ""分頁範例"",
                        ""icon"": ""Setting"",
                        ""url"": ""/PaginationExample""
                    }
                ]
            },
            {
                ""title"": ""課程管理"",
                ""url"": ""2"",
                ""icon"": ""Setting""
            },
            {
                ""url"": ""3"",
                ""title"": ""用戶管理"",
                ""icon"": ""Setting""
            },
            {
                ""url"": ""4"",
                ""title"": ""廣告管理"",
                ""icon"": ""Setting"",
                ""children"": [
                    {
                        ""index"": ""4-1"",
                        ""title"": ""廣告列表"",
                        ""icon"": ""Setting""
                    },
                    {
                        ""index"": ""4-2"",
                        ""title"": ""廣告位列表"",
                        ""icon"": ""Setting""
                    }
                ]
            },
             {
                ""title"": ""權限管理"",
                ""url"": ""5"",
                ""icon"": ""Setting"",
                ""children"": [
                    {
                        ""title"": ""菜單列表"",
                        ""icon"": ""Setting"",
                        ""url"": ""/about""
                    },
                    {
                        ""title"": ""資源列表"",
                        ""icon"": ""Setting"",
                        ""url"": ""/""
                    },
                    {
                        ""title"": ""角色列表"",
                        ""icon"": ""Setting""
                    }
                ]
            }
        ]";

            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = sideBars,
                Msg = string.Empty
            };

            return apiResponse;
        }

        public ApiResponseModel GetMenus() 
        {
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = layOutDal.GetMenus(),
                Msg = string.Empty

            };

            return apiResponse;
        }

        public ApiResponseModel GetNoDisableMeaus()
        {
            var meauResluts = layOutDal.GetSideBarMeauList();
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = meauResluts,
                Msg = string.Empty
            };

            return apiResponse;
        }

        public ApiResponseModel UpdateMeauList(MenuItemEntity menuItem)
        {
            layOutDal.UpdateMeauList(menuItem);

            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = string.Empty,
                Msg = string.Empty
            };

            return apiResponse;
        }

        public ApiResponseModel AddMeauList(MenuItemEntity menuItem)
        {
            layOutDal.AddMeauList(menuItem);
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = string.Empty,
                Msg = string.Empty

            };

            return apiResponse;
        }

        public List<MenuItemEntity> GetSideBarMeauListAuthorize(string Id)
        {
           return  layOutDal.GetSideBarMeauList(Id);
        }

        private List<MenuItem> ConvertToMenuItems(List<MenuItemEntity> entities)
        {
            var menuItems = entities.Select(entity => new MenuItem
            {
                Id = entity.Id,
                Title = entity.Title,
                Url = entity.Url,
                Icon = entity.Icon,
                ParentId = entity.ParentId
            }).ToList();

            // 建立層級結構
            foreach (var menuItem in menuItems)
            {
                menuItem.Children = menuItems.Where(e => e.ParentId == menuItem.Id).ToList();
            }

            return menuItems.Where(e => e.ParentId == null).ToList();
        }
    }
}
