namespace Vibe.Tools.Result
{
    public interface IResult
    {
        public List<Error> Errors { get; }
        public Boolean IsSuccess { get; }
        public Boolean IsFail { get; }
    }
}
