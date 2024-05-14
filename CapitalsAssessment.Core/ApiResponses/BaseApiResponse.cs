namespace CapitalsAssessment.Core.APIResponse
{
    public class BaseResponse<T>
    {
        public BaseResponse() { }

        public BaseResponse(bool succeeded, string message, T result, IEnumerable<string> errors, int statusCode)
        {
            Succeeded = succeeded;
            Message = message;
            Data = result;
            Errors = errors;
            StatusCode = statusCode;
        }

        public BaseResponse(bool succeeded, string message, T result, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Message = message;
            Data = result;
            Errors = errors;
        }

        public BaseResponse(bool succeeded, T data, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Data = data;
            Errors = errors;
        }

        public bool Succeeded { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }
        public int StatusCode { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public static BaseResponse<T> Success(string message, T result, int statusCode)
        {
            return new BaseResponse<T>(true, message, result, new List<string>(), statusCode);
        }

        public static BaseResponse<T> Failure(IEnumerable<string> errors)
        {
            return new BaseResponse<T>(false, default, errors);
        }
    }
}
