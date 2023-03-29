namespace AsyncProductAPI.Models
{
    public class ListingRequest
    {
        public int Id { get; set; }

        public string? RequestBody { get; set; }

        // instead, we should have an inbound Dto and outbound Dto
        public string? EstimatedCompletionTime { get; set; }

        public string? RequestStatus { get; set; }

        public string? RequestId { get; set; } = Guid.NewGuid().ToString();
    }
}