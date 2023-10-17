

namespace API.Helpers
{
    public class LikeParams:PaginationParams
    {
        public int UserId{set;get;}
        public string Predicate{set;get;}
        
    }
}