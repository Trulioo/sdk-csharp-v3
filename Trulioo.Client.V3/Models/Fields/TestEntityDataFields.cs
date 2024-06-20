namespace Trulioo.Client.V3.Models.Fields
{
    /// <summary>
    /// The data field name-value pairs for the data elements on which the verification is to be performed
    /// </summary>
    public class TestEntityDataFields : DataFields
    {
        /// <summary>
        /// Friendly name for the test entity
        /// </summary>
        public string TestEntityName { get; set; }
    }
}
