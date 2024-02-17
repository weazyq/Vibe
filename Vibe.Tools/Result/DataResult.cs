
namespace Vibe.Tools.Result
{
    public partial class Result<T> : IResult
    {
        public T Value { get; }
        public T Data => Value;
        
        public Error? Error { get; }
        public List<Error> Errors => Error switch
        {
            null => new List<Error>(),
            _ => [Error],
        };
        
        public Boolean IsSuccess => Errors.IsEmpty();
        public Boolean IsFail => Errors.IsNotEmpty();


        internal Result(Error? error = null) : this(default!, error) { }
        public Result(T value, Error? error = null)
        {
            Value = value;
            Error = error;
        }

        public static implicit operator T(Result<T> result) => result.Value;
        public static implicit operator Result<T>(T value) => new(value);
        public static implicit operator Result<T>(Result result) => new(result.Errors[0]);
        public static implicit operator Result(Result<T> result) => new(result.Error);

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Fail(T? value, Error error)
        {
            return new Result<T>(value, error);
        }
    }
}
