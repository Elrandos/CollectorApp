namespace inapp.DTOs
{
    public class ErrorDto
    {
        // Kod błędu, np. "BadRequest", "NotFound", "Unauthorized"
        public string ErrorCode { get; set; }

        // Szczegółowy opis błędu
        public string Message { get; set; }
    }
}