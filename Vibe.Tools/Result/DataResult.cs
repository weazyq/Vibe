
namespace Vibe.Tools.Result
{
    public class DataResult<T> : IResult
    {
        public T Value { get; }
        public T Data => Value;
        private Error[] _errors => new Error[] { };
        public List<Error> Errors => _errors.ToList();
        public Boolean IsSuccess => Errors.IsEmpty();
        public Boolean IsFail => Errors.IsNotEmpty();

        public DataResult(T value, Error? error = null)
        {
            Value = value;
        }

        public static DataResult<T> Success(T value)
        {
            return new DataResult<T>(value);
        }

        public static DataResult<T> Fali(T value, Error error)
        {
            return new DataResult<T>(value, error);
        }
    }
}
