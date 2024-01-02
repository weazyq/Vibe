namespace Vibe.Tools.Result
{
    public class Result : IResult
    {
        private Error[] _errors => new Error[] { };

        public List<Error> Errors => _errors.ToList();

        public Boolean IsSuccess => Errors.IsEmpty();

        public Boolean IsFail => Errors.IsNotEmpty();

        public void AddError(Error error)
        {
            Errors.Add(error);
        }

        public static Result Success = new Result() { };

        public static Result Fail(String message) 
        {
            Result result = new Result();
            result.AddError(new Error(message));

            return result;
        }
    }
}
