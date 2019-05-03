namespace Site.Application.Messaging
{
    public interface IMessage
    {
        MessageType Type { get; }
        string MessageName { get; }
    }
    public enum MessageType
    {
        GithubRepoUpdate,
        UserBlogPostsUpdate
    }
}