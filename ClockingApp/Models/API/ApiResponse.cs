using System;
namespace ClockingApp.Models.API
{
	public class ApiResponse
	{
		public bool isSuccess;
		public string responseMessage;

        public ApiResponse(bool isSuccess, string responseMessage)
        {
            this.isSuccess = isSuccess;
            this.responseMessage = responseMessage;
        }
    }
}

