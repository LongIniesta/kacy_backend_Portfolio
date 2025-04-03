using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO.Request;
using Services.DTO.Response;
using Services.Services.ImplementServices;
using Services.Services.InterfaceServices;

namespace Kacy_Backend.Controllers
{
    [Route("api/staff")]
    [ApiController]
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        public StaffController(IStaffService staffService) { 
            _staffService = staffService;
        }

        /// <summary>
        /// Create a new staff
        /// </summary>
        /// <param name="reuqest">The staff information</param>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<StaffResponse>> CreateStaff([FromBody] CreateStaffRequest reuqest)
        {
            var rs = await _staffService.CreateStaff(reuqest);
            return Ok(rs);
        }

        /// <summary>
        /// Get list of staff
        /// </summary>
        /// <param name="pagingRequest"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<StaffResponse>>> GetStaffs([FromQuery] PagingRequest pagingRequest, [FromQuery] StaffRequest request)
        {
            var rs = await _staffService.GetStaffs(request, pagingRequest);
            return Ok(rs);
        }

        /// <summary>
        /// Get a staff by staff's id
        /// </summary>
        /// <param name="staffId">The staff's id</param>
        [Authorize(Policy = "Customer")]
        [HttpGet("{staffId:int}")]
        public async Task<ActionResult<StaffResponse>> GetStaffById(int staffId)
        {
            var rs = await _staffService.GetStaffById(staffId);
            return Ok(rs);
        }

        /// <summary>
        /// Change staff's password
        /// </summary>
        /// <param name="request">The password information</param>
        /// /// <param name="staffId">The staff's id</param>
        [Authorize(Policy = "Staff")]
        [HttpPut("Change-Password/{staffId:int}")]
        public async Task<ActionResult<StaffResponse>> ChangePassword([FromBody] ChangePasswordRequest request, int staffId)
        {
            var rs = await _staffService.ChangePassStaff(request, staffId);
            return Ok(rs);
        }

        /// <summary>
        /// Update staff's information
        /// </summary>
        /// <param name="staffId">The staff's id</param>
        [Authorize(Policy = "Staff")]
        [HttpPut("{staffId:int}")]
        public async Task<ActionResult<StaffResponse>> UpdateStaff([FromBody]StaffUpdateRequest request,int staffId)
        {
            var rs = await _staffService.UpdateStaff(request,staffId);
            return Ok(rs);
        }

        /// <summary>
        /// Update staff's information
        /// </summary>
        /// <param name="staffId">The staff's id</param>
        [Authorize(Policy = "Customer")]
        [HttpGet("byRoomId/{roomId:int}")]
        public async Task<ActionResult<StaffResponse>> getStaffRoomId( int roomId)
        {
            var rs = await _staffService.GetStaffByRoomId(roomId);
            return Ok(rs);
        }

        /// <summary>
        /// Ban a staff
        /// </summary>
        /// <param name="staffId">The staff's id</param>
        [AllowAnonymous]
        [HttpPut("Ban/{staffId:int}")]
        public async Task<ActionResult<StaffResponse>> BanStaff(int staffId)
        {
            var rs = await _staffService.BanStaff(staffId);
            return Ok(rs);
        }
        /// <summary>
        /// Unban a staff
        /// </summary>
        /// <param name="staffId">The staff's id</param>
        [AllowAnonymous]
        [HttpPut("Unban/{staffId:int}")]
        public async Task<ActionResult<StaffResponse>> UnbanStaff(int staffId)
        {
            var rs = await _staffService.UnbanStaff(staffId);
            return Ok(rs);
        }

        /// <summary>
        /// Reset staff's password
        /// </summary>
        /// <param name="email">The staff's email</param>
        [AllowAnonymous]
        [HttpPut("Reset-Password/{email}")]
        public async Task<ActionResult<StaffResponse>> ResetPass(string email)
        {
            var rs = await _staffService.ResetPassword(email);
            return Ok(rs);
        }

        /// <summary>
        /// Change staff's email
        /// </summary>
        /// <param name="request">The staff's email</param>
        [Authorize(Policy = "Staff")]
        [HttpPut("Change-Email")]
        public async Task<ActionResult<StaffResponse>> ChangeMail([FromBody]ChangeMailRequest request)
        {
            var rs = await _staffService.ChangeMail(request);
            return Ok(rs);
        }
    }
}
