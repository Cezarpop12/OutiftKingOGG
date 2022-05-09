namespace OutfitKing.Models
{
    public class ErrorViewModel
    {
        /// <summary>
        /// Models zijn DTO's
        /// </summary>
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}