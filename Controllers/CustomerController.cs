using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO.Request;
using Services.DTO.Response;
using Services.Services.ImplementServices;
using Services.Services.InterfaceServices;

namespace Kacy_Backend.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Verify customer's email
        /// </summary>
        /// <param name="reuqest">Authentication  for customer</param>
        [AllowAnonymous]
        [HttpGet("Verify-Email-Customer/{Email}")]
        public async Task<ActionResult<string>> VerifyEmail(string Email)
        {
            var rs = await _customerService.GetVeiryCodeCustomer(Email);
            return Ok(rs);
        }

        /// <summary>
        /// Sign Up Customer
        /// </summary>
        /// <param name="customer">The customer information for creating a new account.</param>
        /// <returns>A response indicating the result of the account creation process.</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<CustomerResponse>> CreateCustomer([FromBody] CreateCustomerRequest customer)
        {
            var rs = await _customerService.CreateCustomer(customer);
            return Ok(rs);
        }

        /// <summary>
        /// Sign Up Customer with Google
        /// </summary>
        /// <param name="customer">The customer information for creating a new account.</param>
        /// <returns>A response indicating the result of the account creation process.</returns>
        [AllowAnonymous]
        [HttpPost("Google")]
        public async Task<ActionResult<CustomerResponse>> CreateCustomerWithGoogle([FromBody] CreateGoogleAccountRequest request)
        {
            var rs = await _customerService.CreateAccountWithGoogle(request.googleId, request.email, request.fullname);
            return Ok(rs);
        }

        /// <summary>
        /// Get list of customers
        /// </summary>
        /// <param name="pagingRequest"></param>
        /// <param name="accountRequest"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<CustomerResponse>>> GetCustomers([FromQuery] PagingRequest pagingRequest, [FromQuery] CustomerRequest request)
        {
            var rs = await _customerService.GetCustomers(request, pagingRequest);
            return Ok(rs);
        }

        /// <summary>
        /// Get Customer by id
        /// </summary>
        /// <param name="customer">The customer information for creating a new account.</param>
        /// <returns>A response indicating the result of the account creation process.</returns>
        [Authorize(Policy = "Customer")]
        [HttpGet("{customerID:int}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerByID(int customerID)
        {
            var rs = await _customerService.GetCustomerById(customerID);
            return Ok(rs);
        }

        /// <summary>
        /// Change customer's password
        /// </summary>
        /// <param name="request">The password information</param>
        /// /// <param name="customerID">The customer's id</param>
        [Authorize(Policy = "Customer")]
        [HttpPut("Change-Password/{customerID:int}")]
        public async Task<ActionResult<CustomerResponse>> ChangePassword([FromBody] ChangePasswordRequest request, int customerID)
        {
            var rs = await _customerService.ChangePassCustomer(request, customerID);
            return Ok(rs);
        }

        /// <summary>
        /// Update customer information
        /// </summary>
        /// <param name="request">The customer information for update.</param>
        [Authorize(Policy = "Customer")]
        [HttpPut("{customerID:int}")]
        public async Task<ActionResult<CustomerResponse>> UpdateCustomer([FromBody] CustomerUpdateRequest request,int customerID )
        {
            var rs = await _customerService.UpdateCustomer(request,customerID);
            return Ok(rs);
        }


        /// <summary>
        /// Reset customer's password
        /// </summary>
        /// <param name="email">The customer's email</param>
        [AllowAnonymous]
        [HttpPut("Reset-Password/{email}")]
        public async Task<ActionResult<CustomerResponse>> ResetPass(string email)
        {
            var rs = await _customerService.ResetPassword(email);
            return Ok(rs);
        }

        /// <summary>
        /// Change customer's email
        /// </summary>
        /// <param name="request">The customer's email</param>
        [Authorize(Policy = "Customer")]
        [HttpPut("Change-Email")]
        public async Task<ActionResult<CustomerResponse>> ChangeMail([FromBody] ChangeMailRequest request)
        {
            var rs = await _customerService.ChangeMail(request);
            return Ok(rs);
        }

        /// <summary>
        /// Ban a customer
        /// </summary>
        /// <param name="customerId">The customer's id</param>
        [Authorize(Policy ="Admin")]
        [HttpDelete("Deacticve/{customerId:int}")]
        public async Task<ActionResult<CustomerResponse>> DeActiveCustomer(int customerId)
        {
            var rs = await _customerService.DetectiveCustomer(customerId);
            return Ok(rs);
        }

    }
}
