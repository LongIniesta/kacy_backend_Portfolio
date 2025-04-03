using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO.Request;
using Services.DTO.Response;
using Services.Services.InterfaceServices;

namespace Kacy_Backend.Controllers
{
    [Route("api/room")]
    [ApiController]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        /// add Customer into room
        /// </summary>
        /// <param name="request">The infomation </param>
        [Authorize(Policy = "Customer")]
        [HttpPost("Add-Customer-Into-Room")]
        public async Task<ActionResult<CustomerInRoomResponse>> AddCustomerIntoRoom([FromBody] CreateCustomerInRoomRequest request)
        {
            var rs = await _roomService.addCustomerIntoRoom(request);
            return Ok(rs);
        }
        /// <summary>
        /// Create room
        /// </summary>
        /// <param name="request">The room's infomation </param>
        [Authorize(Policy = "Staff")]
        [HttpPost]
        public async Task<ActionResult<RoomResponse>> CreateRoom([FromBody] CreateRoomRequest request)
        {
            var rs = await _roomService.createRoom(request);
            return Ok(rs);
        }

        /// <summary>
        /// Get rooms
        /// </summary>
        /// <param name="request">The room's filter </param>
        [Authorize(Policy = "Customer")]
        [HttpGet]
        public async Task<ActionResult<PagedResults<RoomResponse>>> GetRooms([FromQuery] PagingRequest paging, [FromQuery] RoomRequest request)
        {
            var rs = await _roomService.getRooms(request, paging);
            return Ok(rs);
        }

        /// <summary>
        /// Get customer in room 
        /// </summary>
        /// <param name="request">The room's filter </param>
        [Authorize(Policy = "Customer")]
        [HttpGet("Customer-In-Room")]
        public async Task<ActionResult<PagedResults<CustomerInRoomResponse>>> GetCustomerInRoom([FromQuery] PagingRequest paging, [FromQuery] CustomerInRoomRequest request)
        {
            var rs = await _roomService.getCustomerInRoom(request, paging);
            return Ok(rs);
        }

        /// <summary>
        /// Get room by id
        /// </summary>
        /// <param name="request">The room's id </param>
        [Authorize(Policy = "Customer")]
        [HttpGet("{RoomId:int}")]
        public async Task<ActionResult<RoomResponse>> GetRoomById(int RoomId)
        {
            var rs = await _roomService.getRoomById(RoomId);
            return Ok(rs);
        }

        /// <summary>
        /// Update room
        /// </summary>
        /// <param name="request">The room's information</param>
        [Authorize(Policy = "Customer")]
        [HttpPut("{RoomId:int}")]
        public async Task<ActionResult<RoomResponse>> GetRoomById([FromBody]CreateRoomRequest request ,int RoomId)
        {
            var rs = await _roomService.updateRoom(request,RoomId);
            return Ok(rs);
        }

        /// <summary>
        /// Change room for customer
        /// </summary>
        /// <param name="request">The room change's information</param>
        [Authorize(Policy = "Customer")]
        [HttpPut]
        public async Task<ActionResult<RoomResponse>> ChangeRoom([FromBody] ChangeRoomForCustomerRequest request)
        {
            var rs = await _roomService.changRoomForCustomer(request);
            return Ok(rs);
        }
    }
}
