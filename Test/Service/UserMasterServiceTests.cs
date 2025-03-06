using NSubstitute;
using NUnit.Framework;
using Repository.InterFace;
using Repository.Model;
using Service.Model;
using Services.EnumsModel;
using Services.InterFace;
using Services.Service;

namespace Test.Service
{
    [TestFixture]
    public class UserMasterServiceTests
    {
        private ICrudService<UserMasterEntity> _mockCrudService;
        private IUserMasterDal _mockUserMasterDal;
        private UserMasterService _service;

        [SetUp]
        public void Setup()
        {
            _mockCrudService = Substitute.For<ICrudService<UserMasterEntity>>();
            _mockUserMasterDal = Substitute.For<IUserMasterDal>();
            _service = new UserMasterService(_mockUserMasterDal, _mockCrudService);
        }


        [Test]
        public void GetUserMasters_ShouldReturnExpectedResponse()
        {
            // Arrange
            var expectedResponse = new ApiResponseModel
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = new List<UserMasterEntity>
                {
                    new UserMasterEntity { Id = 1, Name = "User1", Status = 1 },
                    new UserMasterEntity { Id = 2, Name = "User2", Status = 1 }
                },
                Msg = string.Empty
            };

            // 模擬 _mockCrudService 的返回值
            _mockCrudService.GetAll(Arg.Any<object>()).Returns(expectedResponse);

            // Act
            var result = _service.GetUserMasters();

            // Assert
            Assert.That(result, Is.Not.Null, "返回結果不應為 null");
            Assert.That(result.Code, Is.EqualTo(ApiCodeEnum.Success.ToString()), "成功標誌應為 Success");
            Assert.That(result.Data, Is.Not.Null, "返回的數據應與預期一致");
            Assert.That(result.Data, Is.InstanceOf<List<UserMasterEntity>>(), "數據應為用戶列表");

            // 驗證模擬的 GetAll 方法被正確調用一次
             var result1 =_mockCrudService.Received(1).GetAll(Arg.Is<object>(o => o != null));

        }
    }
}
