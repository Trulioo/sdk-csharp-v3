namespace Trulioo.Client.V3.Models.Configuration
{
    /// <summary>
    /// Field info for a datasource
    /// </summary>
    public class Consent
    {
        /// <summary>
        /// Name of the datasource requiring consent
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Text outlining how the user is consenting for their data to be used
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// URL where the user can find more information about how the datasource will use their data
        /// </summary>
        public string Url { get; set; }
    }
}