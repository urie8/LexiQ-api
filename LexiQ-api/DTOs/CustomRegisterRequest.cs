namespace LexiQ_api.DTOs
{
    public class CustomRegisterRequest
    {
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
