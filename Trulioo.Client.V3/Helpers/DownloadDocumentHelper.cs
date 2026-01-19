using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Trulioo.Client.V3.Models.Verification;

namespace Trulioo.Client.V3.Helpers
{
    public static class DownloadDocumentHelper
    {
        public static async Task<DownloadDocument> ParseDownloadDocumentResponseAsync(HttpResponseMessage response)
        {
            var rawMessage = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            var filename = "downloadDocument.pdf";
            if (!string.IsNullOrEmpty(response.Content.Headers.ContentDisposition?.FileName))
            {
                filename = response.Content.Headers.ContentDisposition.FileName;
            }

            return new DownloadDocument
            {
                DocumentName = filename,
                DocumentContent = rawMessage
            };
        }
    }
}
