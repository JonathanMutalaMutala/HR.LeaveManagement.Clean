using AutoMapper;
using Blazored.LocalStorage;

namespace HR.LeaveManagement.BlazorUI.Services.Base
{
    public class BaseHttpService
    {
        protected IClient _client;
        protected readonly IMapper _mapper;
        protected readonly ILocalStorageService _localStorage;
        public BaseHttpService(IClient client,IMapper mapper,ILocalStorageService localStorageService)
        {
            _client = client;
            _mapper = mapper;
            _localStorage = localStorageService;
        }

        public BaseHttpService(IClient client, IMapper mapper)
        {
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
