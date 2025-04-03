using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Services.DTO.Request;
using Services.DTO.Response;
using Services.Services.ImplementServices;
using Services.Services.InterfaceServices;

namespace Kacy_Backend.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _tranService;
        public TransactionController(ITransactionService transactionService)
        {
            _tranService = transactionService;
        }

        /// <summary>
        /// Get list of transaction
        /// </summary>
        /// <param name="pagingRequest"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<TransactionResponse>>> GetTransactions([FromQuery] PagingRequest pagingRequest, [FromQuery] TransactionRequest request)
        {
            var rs = await _tranService.getTransactions(request, pagingRequest);
            return Ok(rs);
        }

        /// <summary>
        /// Get a transaction by transaction's id
        /// </summary>
        /// <param name="tranID">The transaction's id</param>
        [Authorize(Policy = "Admin")]
        [HttpGet("{tranId:int}")]
        public async Task<ActionResult<TransactionResponse>> GetTransactionById(int tranId)
        {
            var rs = await _tranService.getTransactionById(tranId);
            return Ok(rs);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("excel")]
        public async Task<IActionResult> ExportToExcel([FromQuery] int month, [FromQuery] int year)
        {
            List<TransactionResponse> list = (await _tranService.getAllTransactions()).Where(t => t.PaymentedDate.Month == month && t.PaymentedDate.Year == year).ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();

            var worksheet = package.Workbook.Worksheets.Add("Sheet1");

            worksheet.Cells[1, 1].Value = "Title Transaction";
            worksheet.Cells[1, 2].Value = "FullName";
            worksheet.Cells[1, 3].Value = "Description";
            worksheet.Cells[1, 4].Value = "Price";
            worksheet.Cells[1, 5].Value = "Payment Date";

            for (int i = 0; i < list.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = list[i].Title;
                worksheet.Cells[i + 2, 2].Value = list[i].Customer != null ? list[i].Customer.Fullname : "Not defined";
                worksheet.Cells[i + 2, 3].Value = list[i].Description;
                worksheet.Cells[i + 2, 4].Value = list[i].Amount;
                worksheet.Cells[i + 2, 5].Value = list[i].PaymentedDate.ToString("dd/MM/yyyy");
            }

            using (var range = worksheet.Cells[1, 1, 1, 5])
            {
                range.Style.Font.Bold = true;
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }

            var fileName = $"Data_{month}_{year}.xlsx";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "downloads", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await package.SaveAsAsync(fileStream);
            }

            var fileUrl = $"{Request.Scheme}://{Request.Host}/downloads/{fileName}";
            return Ok(new { url = fileUrl });
        }

    }
}

