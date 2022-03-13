using System.Reflection;
using Autofac;
using FantasySoccerManagement.Infrastructure.Data;
using MediatR;
using Module = Autofac.Module;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;
using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagement.Infrastructure.Entity;
using FantasySoccerManagement.Infrastructure.Interfaces;
using FantasySoccerManagement.Core.Interfaces;
using FantasySoccerManagement.Infrastructure.Messaging;
using FantasySoccerManagement.Core.DomainEvents;

namespace FantasySoccerManagement.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        private readonly bool _isDevelopment = false;
        private readonly List<Assembly> _assemblies = new();

        public DefaultInfrastructureModule(bool isDevelopment, Assembly callingAssembly = null)
        {
            _isDevelopment = isDevelopment;

            var coreAssembly = Assembly.GetAssembly(typeof(League));
            _assemblies.Add(coreAssembly);

            var infrastructureAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            _assemblies.Add(infrastructureAssembly);

            if (callingAssembly != null)
            {
                _assemblies.Add(callingAssembly);
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_isDevelopment)
            {
                RegisterDevelopmentOnlyDependencies(builder);
            }
            else
            {
                RegisterProductionOnlyDependencies(builder);
            }
            RegisterCommonDependencies(builder);
        }

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>))
                .InstancePerLifetimeScope();

            // add a cache
            builder.RegisterGeneric(typeof(CachedRepository<>))
              .As(typeof(IReadRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>()
                .As<IIdentityRepository<ApplicationUser>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserService>()
                .As<IIdentityService<ApplicationUser>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PlayerAddedEventPublisher>()
                .As<IMessagePublisher<PlayerAddedEvent>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TeamAddedEventPublisher>()
                .As<IMessagePublisher<TeamAddedEvent>>()
                .InstancePerLifetimeScope();

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                .RegisterAssemblyTypes(_assemblies.ToArray())
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
            }

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }

        private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
        {
            // Add development only services
        }

        private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
        {
            // Add production only services
        }
    }
}
