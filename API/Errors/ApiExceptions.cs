
namespace API.Errors
{
    public class ApiExceptions
    {
        public ApiExceptions(int dstatusCode,string message=null,string details=null)
        {
            StatusCode=dstatusCode;
            Message=message;
            Details=details;
        }
         public int? StatusCode{set;get;}
        public string Message{set;get;}
        public string Details{set;get;}
        
    }
}