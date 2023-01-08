using Autofac;
using Movies.Core.Repositories;
using Movies.Core.Services;
using Movies.Core.UnitOfWorks;
using Movies.Repository;
using Movies.Repository.Repositories;
using Movies.Repository.UnitOfWorks;
using Movies.Service.Services;
using NLayerApp.Service.Mapping;
using System.Reflection;
using Module = Autofac.Module;

namespace Movies.API.Modules
{
    public class RepositoryServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IGenericService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(
                 apiAssembly,
                 repoAssembly,
                 serviceAssembly).
                 Where(x => x.Name.EndsWith("Repository")).
                 AsImplementedInterfaces().
                 InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(
             apiAssembly,
             repoAssembly,
             serviceAssembly).
             Where(x => x.Name.EndsWith("Service")).
             AsImplementedInterfaces().
             InstancePerLifetimeScope();

        }
    }
}
