

namespace API.DTOs
{
    public class MessageDto
    {
        public int Id{set;get;}
        public int SenderId{set;get;}
        public string SenderUsername{get;set;}
        public string SenderPhotoUrl{get;set;}
        public int RecipientId{set;get;}
        public string RecipientUsername{get;set;}
        public string RecipientPhotoUrl{get;set;}
        public string Content{set;get;}
        public DateTime? DateRead{set;get;}
        public DateTime MessageSent{set;get;}
       
        
    }
}