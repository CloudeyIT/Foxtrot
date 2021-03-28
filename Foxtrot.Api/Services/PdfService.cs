using System.IO;
using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace Foxtrot.Api.Services
{
    public class PdfService
    {
        private readonly Browser _browser;
        public PdfService (Browser browser) { _browser = browser; }

        public async Task<Stream> GetPdfFromHtml (string html)
        {
            await using var page = await _browser.NewPageAsync();
            await page.SetContentAsync(html);
            
            return await GetPdfStream(page);
        }

        public async Task<Stream> GetPdfFromUrl (string url)
        {
            await using var page = await _browser.NewPageAsync();
            await page.GoToAsync(url);

            return await GetPdfStream(page);
        }

        private static async Task<Stream> GetPdfStream (Page page)
        {
            return await page.PdfStreamAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                MarginOptions = new MarginOptions
                {
                    Top = "20px",
                    Right = "20px",
                    Bottom = "40px",
                    Left = "20px"
                },
            });
        }
    }
}