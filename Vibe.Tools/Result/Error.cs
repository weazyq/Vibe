namespace Vibe.Tools.Result
{
    public class Error
    {
        public String Message { get; set; }
        public String? Key { get; set; }

        public Error(String message, String? key) 
        {
            Message = message;
            Key = key;
        }
    }
}
