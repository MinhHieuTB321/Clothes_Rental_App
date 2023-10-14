using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserService.Application.ViewModels;
using UserService.Application.ViewModels.Orders;
using UserService.Application.ViewModels.Users;
using UserService.Domain.Entities;

namespace UserService.Application.EventProcessing
{
    enum EventType
    {
        OrderPublished,
        CustomerPublished,
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
                case EventType.CustomerPublished:
                    await AddCustomer(message);
                    break;
                default:
                    break;
            }
        }

        private async Task AddCustomer(string message)
        {
            using(var scope= _scopeFactory.CreateScope())
            {
                var unitOfWork= scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var customerPublishedModel = JsonSerializer.Deserialize<CustomerPublishedModel>(message);

                try
                {   
                    var user= _mapper.Map<User>(customerPublishedModel);
                    if(await unitOfWork.UserRepository.GetByIdAsync(user.Id) ==null)
                    {
                        await unitOfWork.UserRepository.AddAsync(user);
                        await unitOfWork.SaveChangeAsync();
                        Console.WriteLine($"--> user added!");
                    }
                }   
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not add user to Db {ex.Message}");
                }
            }
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
                case "Customer_Published":
                    Console.WriteLine("-->Customer Published Event Detected");
                    return EventType.CustomerPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

    }
}
