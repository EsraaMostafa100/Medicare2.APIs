namespace Medicare2.APIs.Errors
{
    public class ApiResponse
    {
     
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int statuscode, string? message = null)
        {
            StatusCode = statuscode;
            Message = message ?? GetDefaultMessageForstatusCode(StatusCode);
        }

        private string? GetDefaultMessageForstatusCode(int? statusCode)
        {
            return StatusCode switch
            {
                400 => "Bad Requset",
                401 => "You are not authorized",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}
