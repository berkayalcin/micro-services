using System.Reflection;
using System.Threading.Tasks;
using MicroServices.Common.Commands;
using MicroServices.Common.Events;
using RawRabbit;
using RawRabbit.Pipe;

namespace MicroServices.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus,
            ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
           return bus.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(
                    cfg => cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));
        }

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus,
            IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            return bus.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(
                    cfg => cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));
        }
        public static string GetQueueName<T>()
        {
            return $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
        }
    }
}