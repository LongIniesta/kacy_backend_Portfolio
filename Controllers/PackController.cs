using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO.Request;
using Services.DTO.Response;
using Services.Services.InterfaceServices;

namespace Kacy_Backend.Controllers
{
    [Route("api/pack")]
    [ApiController]
    public class PackController : Controller
    {
        private readonly IPackService _PackService;
        private const string ADMIN = "Admin";
        private const string CUSTOMER = "Customer";
        private const string STAFF = "Staff";
        public PackController(IPackService PackService)
        {
            _PackService = PackService;
        }

        /// <summary>
        /// Create an Pack
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = ADMIN)]
        [HttpPost]
        public async Task<ActionResult<PackResponse>> createPack([FromBody] CreatePackRequest request)
        {
            var rs = await _PackService.CreatePack(request);
            return Ok(rs);
        }

        /// <summary>
        /// Buy pack
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = CUSTOMER)]
        [HttpPost("Buy-Pack")]
        public async Task<ActionResult<PackOfCustomerResponse>> buyPack([FromBody] BuyPackAndCreateTransactionRequest request)
        {
            var rs = await _PackService.BuyPack(request);
            return Ok(rs);
        }

        /// <summary>
        /// Extend pack
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = CUSTOMER)]
        [HttpPut("Extend-Pack")]
        public async Task<ActionResult<PackOfCustomerResponse>> Extend([FromBody] BuyPackAndCreateTransactionRequest request)
        {
            var rs = await _PackService.ExtendPack(request);
            return Ok(rs);
        }

        /// <summary>
        /// Get an Pack by Pack's id
        /// </summary>
        /// <param name="PackId">Pack's id</param>
        [Authorize(Policy = CUSTOMER)]
        [HttpGet("{PackId:int}")]
        public async Task<ActionResult<PackResponse>> getPackById(int PackId)
        {
            var rs = await _PackService.GetPackById(PackId);
            return Ok(rs);
        }

        /// <summary>
        /// Get an Pack by customer's id
        /// </summary>
        /// <param name="customerId">Customer's id</param>
        [AllowAnonymous]
        [HttpGet("Get-By-Customer/{customerId:int}")]
        public async Task<ActionResult<PackOfCustomerResponse>> getPackByCusId(int customerId)
        {
            var rs = await _PackService.GetPackByCustomerId(customerId);
            return Ok(rs);
        }

        /// <summary>Get list Packs</summary>
        /// <param name="request">Filter</param>
        /// <param name="paging">Paging</param>
        [Authorize(Policy = CUSTOMER)]
        [HttpGet]
        public async Task<ActionResult<PagedResults<PackResponse>>> getPacks([FromQuery] PackRequest request, [FromQuery] PagingRequest paging)
        {
            var rs = await _PackService.GetPacks(request, paging);
            return Ok(rs);
        }

        /// <summary>Update an Pack</summary>
        /// <param name="request">Pack's information</param>
        /// <param name="PackId">Pack's Id</param>
        [Authorize(Policy = ADMIN)]
        [HttpPut("PackId:int")]
        public async Task<ActionResult<PackResponse>> updatePack([FromBody] CreatePackRequest request, int PackId)
        {
            var rs = await _PackService.UpdatePack(request, PackId);
            return Ok(rs);
        }

        ///<summary>Delete an Pack</summary>
        ///<param name="PackId">Pack's Id</param>
        [Authorize(Policy = ADMIN)]
        [HttpDelete("PackId:int")]
        public async Task<ActionResult<PackResponse>> deletePack(int PackId)
        {
            var rs = await _PackService.DeletePackById(PackId);
            return Ok(rs);
        }
    }
}
