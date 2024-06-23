﻿namespace Medicare2.APIs.Errors
{
    public class ApiValidationResponse :ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationResponse() : base(400)
        {
            Errors = new List<string>();
        }
    }
}