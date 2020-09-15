using MediatR;

namespace Site.Application.PlatformAccounts.Commands
{
  public class UpdateAccountStreamStateCommand : IRequest<bool>
  {
    public string AccountId { get; set; }
    public bool IsStreaming { get; set; }
    public int UserId { get; set; }
  }
}
