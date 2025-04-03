using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO.Request;
using Services.DTO.Response;
using Services.Services.InterfaceServices;

namespace Kacy_Backend.Controllers
{
    [Route("api/authen")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IStaffService _staffService;
        private readonly IAdminServices _adminServices;
        public AuthenticationController(ICustomerService customerService, IStaffService staffService, IAdminServices adminServices)
        {
            _customerService = customerService;
            _staffService = staffService;
            _adminServices = adminServices;
        }

        /// <summary>
        /// Create admin account
        /// </summary>
        /// <param name="reuqest">Authentication  for customer</param>
        [AllowAnonymous]
        [HttpPost("Create-Admin")]
        public async Task<ActionResult<CustomerResponse>> CreateAdmin()
        {
            var rs = await _adminServices.CreateAdmin();
            return Ok(rs);
        }



        /// <summary>
        /// Admin Login
        /// </summary>
        /// <param name="reuqest">Authentication  for admin</param>
        [AllowAnonymous]
        [HttpPost("Authentication-Admin")]
        public async Task<ActionResult<AdminResponse>> LoginAdmin([FromBody] LoginAdminRequest reuqest)
        {
            var rs = await _adminServices.LoginAdmin(reuqest);
            return Ok(rs);
        }

        /// <summary>
        /// Customer Login
        /// </summary>
        /// <param name="reuqest">Authentication  for customer</param>
        [AllowAnonymous]
        [HttpPost("Authentication-Customer")]
        public async Task<ActionResult<CustomerResponse>> LoginCustomer([FromBody] LoginCustomerRequest reuqest)
        {
            var rs = await _customerService.LoginCustomer(reuqest);
            return Ok(rs);
        }
        
        /// <summary>
        /// Staff Login
        /// </summary>
        /// <param name="reuqest">Authentication  for staff</param>
        [AllowAnonymous]
        [HttpPost("Authentication-Staff")]
        public async Task<ActionResult<StaffResponse>> LoginStaff([FromBody] LoginStaffRequest reuqest)
        {
            var rs = await _staffService.LoginStaff(reuqest);
            return Ok(rs);
        }

        /// <summary>
        /// Google Login
        /// </summary>
        /// <param name="reuqest">Authentication  for google</param>
        [AllowAnonymous]
        [HttpPost("Authentication-Google")]
        public async Task<ActionResult<CustomerResponse>> LoginGoogle([FromBody] LoginGoogleRequest request)
        {
            var rs = await _customerService.LoginGoogle(request.googleId, request.email);
            return Ok(rs);
        }

        /// <summary>
        /// Generate new access token and refresh token when they are expired for admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Token-Verification-Admin")]
        public async Task<ActionResult<AdminResponse>> VerifyAndGenerateTokenAdmin(TokenRequest request)
        {
            var rs = await _adminServices.RefreshAdminToken(request);
            return Ok(rs);
        }

        /// <summary>
        /// Generate new access token and refresh token when they are expired for customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Token-Verification-Customer")]
        public async Task<ActionResult<CustomerResponse>> VerifyAndGenerateTokenCustomer(TokenRequest request)
        {
            var rs = await _customerService.VerifyAndGenerateToken(request);
            return Ok(rs);
        }
        /// <summary>
        /// Generate new access token and refresh token when they are expired for Staff
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Token-Verification-Staff")]
        public async Task<ActionResult<StaffResponse>> VerifyAndGenerateTokenStaff(TokenRequest request)
        {
            var rs = await _staffService.VerifyAndGenerateToken(request);
            return Ok(rs);
        }

        /// <summary>
        /// Use for logout to revoke old Admin's token
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpPost("Revoke-Token-Admin")]
        public async Task<ActionResult<CustomerResponse>> RevokeTokenAdmin()
        {
            var rs = await _adminServices.RevokeAdminToken();
            return Ok(rs);
        }

        /// <summary>
        /// Use for logout to revoke old Customer's token
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [Authorize(Policy = "Customer")]
        [HttpPost("Revoke-Token-Customer/{customerID:int}")]
        public async Task<ActionResult<CustomerResponse>> RevokeTokenCustomer(int customerID)
        {
            var rs = await _customerService.RevokeRefreshToken(customerID);
            return Ok(rs);
        }

        /// <summary>
        /// Use for logout to revoke old Staff's token
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        [Authorize(Policy = "Staff")]
        [HttpPost("Revoke-Token-Staff/{staffId:int}")]
        public async Task<ActionResult<StaffResponse>> RevokeTokenStaff(int staffId)
        {
            var rs = await _staffService.RevokeRefreshToken(staffId);
            return Ok(rs);
        }

        /// <summary>
        /// Test Authen customer
        /// </summary>
        [Authorize(Policy = "Customer")]
        [HttpGet("Test-Customer")]
        public async Task<ActionResult> TestAuthen() {
            return Ok("oke nha");
        }

        /// <summary>
        /// Test Authen Staff
        /// </summary>
        [Authorize(Policy = "Staff")]
        [HttpGet("Test-Staff")]
        public async Task<ActionResult> TestAuthenStaff()
        {
            return Ok("oke nha");
        }

        /// <summary>
        /// Test Authen Staff And Customer
        /// </summary>
        [Authorize(Policy = "User")]
        [HttpGet("Test-User")]
        public async Task<ActionResult> TestAuthenStaffAndCustomer()
        {
            return Ok("oke nha");
        }
    }
}
