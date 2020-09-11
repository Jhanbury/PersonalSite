using DryIoc;

namespace Site.Application.Infrastructure
{
  public interface IModule
  {
    void Load(IRegistrator builder);
  }
}
