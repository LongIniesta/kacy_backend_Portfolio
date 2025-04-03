using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO.Request;
using Services.DTO.Response;
using Services.Services.ImplementServices;
using Services.Services.InterfaceServices;

namespace Kacy_Backend.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : Controller
    {

        private readonly IAdminServices _adminServices;
        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        /// <summary>
        /// Change admin's password
        /// </summary>
        /// <param name="request">The password information</param>
        [Authorize(Policy = "Admin")]
        [HttpPut("Change-Password")]
        public async Task<ActionResult<bool>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var rs = await _adminServices.ChangePassword(request);
            return Ok(rs);
        }

        /// <summary>
        /// Reset admin's password
        /// </summary>
        /// <param name="email">The admin's email</param>
        [AllowAnonymous]
        [HttpPut("Reset-Password/{email}")]
        public async Task<ActionResult<AdminResponse>> ResetPass(string email)
        {
            var rs = await _adminServices.ResetPassword(email);
            return Ok(rs);
        }

        /// <summary>
        /// Change admin's email
        /// </summary>
        /// <param name="request">The admin's email</param>
        [Authorize(Policy = "admin")]
        [HttpPut("Change-Email")]
        public async Task<ActionResult<AdminResponse>> ChangeMail([FromBody] ChangeMailRequest request)
        {
            var rs = await _adminServices.ChangeMail(request);
            return Ok(rs);
        }
    }
}
