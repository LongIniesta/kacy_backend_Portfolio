using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTO.Request;
using Services.DTO.Response;
using Services.Services.ImplementServices;
using Services.Services.InterfaceServices;

namespace Kacy_Backend.Controllers
{
    [Route("api/analysis")]
    [ApiController]
    public class AnalysisController : Controller
    {
        private readonly IAnalysisService _analysisService;
        public AnalysisController(IAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        /// <summary>
        /// Get analysis
        /// </summary>
        /// <param name="request">The filter</param>
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<ActionResult<AnalysisRespone>> GetAnalysis(IFormFile file)
        {
            var rs = await _analysisService.getAnalysis(file);
            return Ok(rs);
        }
    }
}
