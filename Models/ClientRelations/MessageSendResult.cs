namespace DYV.Models.ClientRelations
{
    public interface IMessageSendResult<T> where T:MessageGroupResult
    {
        int Id { get; set; }
        bool Success { get; set; }
        string Error { get; set; }
        bool SlugOpened { get; set; }
        bool RecipientSignedUp { get; set; }
        string UniqueSignupSlug { get; set; }
        int GroupSendResultId { get; set; }
        T GroupSendResult { get; set; }
    }

    public abstract class MessageSendResult
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
        public bool SlugOpened { get; set; }
        public bool RecipientSignedUp { get; set; }
        public string UniqueSignupSlug { get; set; }
    }
}