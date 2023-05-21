using EmainesChat.Business.Commands;
using EmainesChat.Business.Rooms;
using EmainesChat.Business.Users;
using Microsoft.AspNetCore.Mvc;

namespace EmainesChat.API.Controllers
{
    [ApiController]
    [Route("Room")]
    public class RoomController : Controller
    {

        private readonly RoomService _roomService;

        private readonly ILogger<UserController> _logger;

        public RoomController(ILogger<UserController> logger, RoomService roomService)
        {
            _logger = logger;
            _roomService = roomService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllRooms()
        {
            return Ok(_roomService.GetAll());
        }

        [HttpGet]
        [Route("{id}/detail")]
        public IActionResult GetRoomById(int id) 
        {
            return Ok(_roomService.GetById(id));
        }

        [HttpGet]
        [Route("{name}/search-by-name")]
        public IActionResult GetRoomByName(string name)
        {
            return Ok(_roomService.GetByName(name));
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateRoomAsync([FromBody]RoomCreateCommand command) 
        {
            return Ok(await _roomService.Create(command));
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateRoomAsync([FromBody]RoomUpdateCommand command)
        {
            return Ok(await _roomService.Update(command));
        }
    }
}
