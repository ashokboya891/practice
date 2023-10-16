

namespace API.Helpers
{
    public class UserParams:PaginationParams
    {
        // private const int MaxPageSize=50;
        // public int PageNumber{set;get;}=1;
        // private int _pageSize=10;

        // public int PageSize
        // {
        //     get =>  _pageSize;
        //     set => _pageSize=(value>MaxPageSize)? MaxPageSize:value;
        // }
    public string CurrentUser{set;get;}
    public string Gender{set;get;}
     public int MinAge{set;get;}=18;
    public string OrderBy{set;get;}="lastActive";
     public int MaxAge{set;get;}=100;
        
    }
  
}