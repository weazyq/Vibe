using System.Text.Json.Serialization;

namespace Vibe.Tools.Result
{
    public partial class Result : IResult
    {
        public Error? Error { get; private set; }

        [JsonPropertyName("errors")]
        public List<Error> Errors { get; }

        public Boolean IsSuccess => Errors.IsEmpty();

        public Boolean IsFail => Errors.IsNotEmpty();

        public Result(Error? error = null)
        {
            Error = error;
            Errors = Error switch
            {
                null => new(),
                _ => new List<Error> { Error }
            };
        }

        [JsonConstructor]
        public Result(List<Error>? errors)
        {
            if (errors is not null) { 
                Errors = errors;
            } else
            {
                Errors = new List<Error>();
            }
        }

        public void AddError(Error error)
        {
            Errors.Add(error);
        }

        public void AddErrors(List<Error> errors)
        {
            errors.ForEach(AddError);
        }

        public static Result Success = new Result() { };

        public static Result Fail(String message, String? key = null) 
        {
            Result result = new Result(new Error(message, key));
            return result;
        }

        public static Result Fail(List<Error> errors)
        {
            Result result = new Result();
            result.AddErrors(errors);
            return result;
        }
    }
}
