using System.Threading.Tasks;
using Foxtrot.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Foxtrot.Api.Controllers
{
    [ApiController]
    [Route("pdf")]
    public class PdfController : ControllerBase
    {
        private readonly PdfService _pdfService;
        public PdfController (PdfService pdfService) { _pdfService = pdfService; }

        [HttpGet]
        [Route("from-html")]
        public async Task<ActionResult> FromHtmlGet ([FromQuery] string html)
        {
            var pdf = await _pdfService.GetPdfFromHtml(html);
            return File(pdf, PdfConstants.ContentType);
        }
        
        [HttpPost]
        [Route("from-html")]
        public async Task<ActionResult> FromHtmlPost ([FromBody] string html)
        {
            var pdf = await _pdfService.GetPdfFromHtml(html);
            return File(pdf, PdfConstants.ContentType);
        }
        
        [HttpGet]
        [Route("from-url")]
        public async Task<ActionResult> FromUrlGet ([FromQuery] string url)
        {
            var pdf = await _pdfService.GetPdfFromUrl(url);
            return File(pdf, PdfConstants.ContentType);
        }
        
        [HttpPost]
        [Route("from-url")]
        public async Task<ActionResult> FromUrlPost ([FromBody] string url)
        {
            var pdf = await _pdfService.GetPdfFromUrl(url);
            return File(pdf, PdfConstants.ContentType);
        }
    }
}