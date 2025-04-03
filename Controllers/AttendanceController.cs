using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.DTO.Request;
using Services.DTO.Response;
using Services.Services.ImplementServices;
using Services.Services.InterfaceServices;

namespace Kacy_Backend.Controllers
{
    [Route("api/attendance")]
    [ApiController]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        /// <summary>
        /// Check attendances
        /// </summary>
        /// <param name="request">The attendance's infomation </param>
        [Authorize(Policy = "Staff")]
        [HttpPost("Check-Attendance")]
        public async Task<ActionResult<PagedResults<AttendanceResponse>>> CheckAttendance([FromBody] List<CheckAttendanceRequest> request)
        {
            var rs = await _attendanceService.checkAttendances(request);
            return Ok(rs);
        }

        /// <summary>
        /// Get attendances
        /// </summary>
        /// <param name="request">The filter</param>
        [Authorize(Policy = "Customer")]
        [HttpGet]
        public async Task<ActionResult<PagedResults<AttendanceResponse>>> GetAttendance([FromQuery] PagingRequest paging, [FromQuery] AttendanceRequest request)
        {
            var rs = await _attendanceService.getAttendance(request, paging);
            return Ok(rs);
        }

        /// <summary>
        /// Get attendances by admin
        /// </summary>
        /// <param name="request">The filter</param>
        [Authorize(Policy = "Customer")]
        [HttpGet("getByAdmin")]
        public async Task<ActionResult<PagedResults<AttendanceResponse>>> GetAttendance([FromQuery] DateTime inDate , [FromQuery] int roomId, [FromQuery] PagingRequest paging)
        {
            var rs = await _attendanceService.getAttendanceByAdmin(inDate, roomId, paging);
            return Ok(rs);
        }

        /// <summary>
        /// Get attendances
        /// </summary>
        /// <param name="request">The filter</param>
        [Authorize(Policy = "Customer")]
        [HttpGet("total/{customerId:int}")]
        public async Task<ActionResult<TotalAttendanceRespone>> GetAttendanceTotal(int customerId)
        {
            var rs = await _attendanceService.getTotalAttendance(customerId);
            return Ok(rs);
        }

    }
}
