using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.Interfaces;
using Site.Domain.Entities;

namespace Site.Application.PlatformAccounts.Commands
{
  public class UpdateAccountStreamStateCommandHandler : IRequestHandler<UpdateAccountStreamStateCommand, bool>
  {
    private readonly IRepository<PlatformAccount, int> _repository;
    private readonly IMapper _mapper;

    public UpdateAccountStreamStateCommandHandler(IRepository<PlatformAccount, int> repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateAccountStreamStateCommand request, CancellationToken cancellationToken)
    {
      return false;
    }
  }
}
