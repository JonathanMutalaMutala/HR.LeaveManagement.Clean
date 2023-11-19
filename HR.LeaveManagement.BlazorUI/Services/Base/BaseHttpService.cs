namespace HR.LeaveManagement.BlazorUI.Services.Base
{
    public class BaseHttpService
    {
        protected IClient _client;

        public BaseHttpService(IClient client)
        {
            _client = client;
        }


        protected Response<Guid> ConvertApiException<Guid>(ApiException exception)
        {
            Response<Guid> response = new Response<Guid>();
            if(exception.StatusCode == 400)
            {
                response = new Response<Guid>
                {
                    Message = "Invalid Data était soumis",
                    ValidationsErrors = exception.Response,
                    Success = false
                };
            }
            else if(exception.StatusCode == 404)
            {
                response = new Response<Guid>
                {
                    Message = "The record was not found",
                    Success = false
                };
            }
            else
            {
                response = new Response<Guid>()
                {
                    Message = "Something went wrong, please try again later",
                    Success = false
                };
            }
            return response;
        }
    }
}
