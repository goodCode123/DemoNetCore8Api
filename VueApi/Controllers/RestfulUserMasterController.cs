using Microsoft.AspNetCore.Mvc;
using Repository.Model;
using Services.InterFace;

namespace DemoNetCore8Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestfulUserMasterController : ControllerBase
    {
        private readonly IUserMasterService _userMasterService;

        public RestfulUserMasterController(IUserMasterService userMasterService)
        {
            _userMasterService = userMasterService;

        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = _userMasterService.GetUserMasters();
            return Ok(result);
        }
        

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = _userMasterService.GetUserMastersById(id);
            return Ok(result);
        }

        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="userMaster"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] UserMasterEntity userMaster)
        {
            var result = _userMasterService.Add(userMaster);
            return Ok(result);
        }

        /// <summary>
        /// 更新使用者
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userMaster"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, UserMasterEntity userMaster)
        {
            if (id != userMaster.Id) return NotFound();
            var result = _userMasterService.Update(userMaster);

            return Ok(result);
        }

        /// <summary>
        /// 刪除使用者
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _userMasterService.Delete(id);
            return Ok(result);
        }
    }
}
