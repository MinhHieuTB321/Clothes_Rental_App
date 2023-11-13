using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using UserService.Application.ViewModels;
using UserService.Application.ViewModels.Orders;
using UserService.Domain.Entities;

namespace UserService.Application.EventProcessing
{
    enum EventType
    {
        OrderPublished,
        UpdatedOrder,
        DeletedOrder,
        Undetermined
    }
    public class EventProcessor:IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory,
            IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public async Task ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch (eventType)
            {
                case EventType.OrderPublished:
                    await AddOrder(message);
                    break;
                case EventType.UpdatedOrder:
                    await UpdateOrder(message);
                    break;
                case EventType.DeletedOrder:
                    await DeleteOrder(message);
                    break;
                default:
                    break;
            }
        }

        private async Task DeleteOrder(string message)
        {
            using(var scope= _scopeFactory.CreateScope())
            {
                var unitOfWork= scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var orderReadModel = JsonSerializer.Deserialize<OrderReadModel>(message);

                try
                {   
                    var order=await unitOfWork.OrderRepository.GetByIdAsync(orderReadModel!.Id);
                    if(order!=null)
                    {
                        unitOfWork.OrderRepository.SoftRemove(order);
                        await unitOfWork.SaveChangeAsync();
                        Console.WriteLine($"--> order Deleted!");
                    }
                }   
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not add Order  to Db {ex.Message}");
                }
            }
        }

        private Task UpdateOrder(string message)
        {
            throw new NotImplementedException();
        }

        private async Task AddOrder(string message)
        {
            using(var scope= _scopeFactory.CreateScope())
            {
                var unitOfWork= scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var orderReadModel = JsonSerializer.Deserialize<OrderReadModel>(message);

                try
                {   
                    var order= _mapper.Map<Order>(orderReadModel);
                    if(await unitOfWork.OrderRepository.GetByIdAsync(order.Id) ==null)
                    {
                        await unitOfWork.OrderRepository.AddAsync(order);
                        await unitOfWork.SaveChangeAsync();
                        Console.WriteLine($"--> order added!");
                    }
                }   
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not add Order  to Db {ex.Message}");
                }
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");
            //Console.WriteLine(notificationMessage);
            var eventType = JsonSerializer.Deserialize<GenericEventModel>(notificationMessage);
            //Console.WriteLine(eventType);
            switch (eventType!.Event)
            {
                case "Order_Published":
                    Console.WriteLine("-->Order Published Event Detected");
                    return EventType.OrderPublished;
                case "Order_Update":
                    Console.WriteLine("-->Order Updated Event Detected");
                    return EventType.UpdatedOrder;
                case "Delete_Order":
                    Console.WriteLine("-->Order Deleted Event Detected");
                    return EventType.DeletedOrder;
                default:
                    Console.WriteLine("--> No Event Type Detected!");
                    return EventType.Undetermined;
            }
        }

    }
}
