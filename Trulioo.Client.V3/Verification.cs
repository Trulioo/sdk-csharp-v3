using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Trulioo.Client.V3.Models.Verification;
using Trulioo.Client.V3.URI;

namespace Trulioo.Client.V3
{
    /// <summary>
    /// Provides a class for working with Trulioo Verification.
    /// </summary>
    public class Verification
    {
        #region Private Properties

        private const string _defaultDocumentFilename = "downloadDocument.pdf";

        private TruliooApiClient _service;
        private readonly Namespace _verificationNamespace = new Namespace("verifications");

        private Context _context
        {
            get { return _service.Context; }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Verification"/> class.
        /// </summary>
        /// <param name="service">
        /// An object representing the root of Trulioo verification service.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="service"/> is <c>null</c>.
        /// </exception>
        protected internal Verification(TruliooApiClient service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        #endregion

        #region Methods

        /// <summary>
        /// The verification call for the Trulioo API V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns> Verification results </returns>
        public async Task<VerifyResult> VerifyAsync(VerifyRequest request)
        {
            var resource = new ResourceName("verify");
            var response = await _context.PostAsync<VerifyResult>(_verificationNamespace, resource, request).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets transaction record information
        /// </summary>
        /// <param name="transactionRecordId">The ID of the transaction record (TRID). This is a GUID</param>
        /// <returns></returns>
        public async Task<TransactionRecordResult> GetTransactionRecordAsync(string transactionRecordId)
        {
            var resource = new ResourceName("transactionrecord", transactionRecordId);
            var response = await _context.GetAsync<TransactionRecordResult>(_verificationNamespace, resource).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets the status of a transaction
        /// </summary>
        /// <param name="transactionId">TransactionID of the Transaction Status to be retreived </param>
        /// <returns> Transaction Status of the transactionID </returns>
        public async Task<TransactionStatus> GetTransactionStatusAsync(string transactionId)
        {
            var resource = new ResourceName("transaction", transactionId, "status");
            var response = await _context.GetAsync<TransactionStatus>(_verificationNamespace, resource).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method is used to retrieve the partial result of an asynchronous transaction.
        /// </summary>
        /// <param name="transactionId">id of the asynchronous transaction, this will be a GUID</param>
        /// <returns>Partial Verify Result</returns>
        public async Task<VerifyResultPartial> GetPartialResultsAsync(string transactionRecordId)
        {
            var resource = new ResourceName("transaction", transactionRecordId, "partialresult");
            var response = await _context.GetAsync<VerifyResultPartial>(_verificationNamespace, resource).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Download Document
        /// </summary>
        /// <param name="transactionRecordId">id of the transactionrecord, this will be a GUID</param>
        /// <param name="fieldName">document field name</param>
        /// <returns>document</returns>
        public async Task<DownloadDocument> GetDocumentDownloadAsync(string transactionRecordId, string fieldName)
        {
            var resource = new ResourceName("documentdownload", transactionRecordId, fieldName);
            var response = await _context.GetAsync(_verificationNamespace, resource, processResponse: parseDownloadDocumentResponse).ConfigureAwait(false);
            return await response;
        }

        /// <summary>
        /// This method is used to retrieve the document of a verification performed using the verify method.
        /// The response for this method includes the processed base64 JPEG formatted string
        /// </summary>
        /// <param name="transactionRecordID">The ID of the transaction record (TRID). This is a GUID</param>
        /// <param name="documentField">FieldName of the Document, this will be a string</param>
        /// <returns>string</returns>
        public async Task<string> GetTransactionRecordDocumentAsync(string transactionRecordId, string documentField)
        {
            var resource = new ResourceName("transactionrecord", transactionRecordId, documentField);
            var responseTask = await _context.GetAsync(_verificationNamespace, resource, parseTransactionRecordDocumentResponse).ConfigureAwait(false);
            return await responseTask;
        }
        #endregion

        #region Response Processing Methods

        private async Task<DownloadDocument> parseDownloadDocumentResponse(HttpResponseMessage response)
        {
            var rawMessage = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            var filename = _defaultDocumentFilename;
            if (response.Content.Headers.ContentDisposition?.FileName != null)
            {
                filename = response.Content.Headers.ContentDisposition.FileName;
            }

            return new DownloadDocument
            {
                DocumentName = filename,
                DocumentContent = rawMessage
            };
        }

        /// <summary>
        /// Processes the string content from GetTransactionRecordDocument + Deserialize to escaped duplicate double quotes
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<string> parseTransactionRecordDocumentResponse(HttpResponseMessage response)
        {
            var message = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            message = JsonConvert.DeserializeObject<string>(message);
            return message;
        }

        #endregion
    }
}
