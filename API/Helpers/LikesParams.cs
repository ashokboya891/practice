
namespace API.Helpers
{
    public class LikesParams:PaginationParams
    {
        public int UserId{set;get;}
        public string Predicate{set;get;}  
    }
}