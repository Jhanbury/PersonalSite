using System;
using DryIoc;
using Site.Application.Infrastructure;
using Site.Application.Interfaces;
using Site.Infrastructure;
using Site.Infrastructure.MessageHandlers;
using Site.Infrastructure.Modules;
using Site.Infrastructure.Services;
using Site.Persistance.Repository;

namespace Personal_Site_API
{
  public class CompositionRoot
  {
    public CompositionRoot(IContainer builder)
    {
      builder.RegisterMany<ApplicationModule>();
      LoadModules(builder);
    }

    private void LoadModules(IContainer builder)
    {
      foreach (var module in builder.ResolveMany<IModule>())
        module.Load(builder);
    }
  }
}
