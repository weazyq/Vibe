namespace Vibe.Tools.Result
{
    public class Error
    {
        public String Message { get; set; }

        public Error(String message) 
        {
            Message = message;
        }
    }
}
