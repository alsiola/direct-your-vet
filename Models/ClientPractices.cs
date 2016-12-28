using DYV.Models.User;

namespace DYV.Models
{
    public class ClientPractices
    {
        public string ClientUserId { get; set; }
        public ClientUser ClientUser { get; set; }
        public int PracticeId { get; set; }
        public Practice Practice { get; set; }
    }
}
