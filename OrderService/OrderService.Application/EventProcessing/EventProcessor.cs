using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.ViewModels;
using OrderService.Application.ViewModels.Customers;
using OrderService.Application.ViewModels.Orders;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderService.Application.EventProcessing
{
    enum EventType
    {
        OrderUpdatePublished,
        Undetermined
    }
    public class EventProcessor : IEventProcessor
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
                case EventType.OrderUpdatePublished:
                    await UpdateOrder(message);
                    break;
                default:
                    break;
            }
        }

        private async Task UpdateOrder(string message)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var orderUpdate = JsonSerializer.Deserialize<OrderUpdatePublished>(message);

                try
                {
                    //var user = _mapper.Map<User>(customerPublishedModel);
                    var order = await unitOfWork.OrderRepository.GetByIdAsync(orderUpdate!.Id, x => x.OrderDetails!);
                    if (order!=null)
                    {

                        order.Status = OrderEnum.Paid.ToString();
                        unitOfWork.OrderRepository.Update(order);
                        var orderDetails = order.OrderDetails!.ToList();
                        for (int i = 0; i < orderDetails!.Count; i++)
                        {
                            orderDetails[i].Status=OrderDetailEnum.IsRented.ToString();
                        }
                        unitOfWork.OrderDetailRepository.UpdateRange(orderDetails);
                        await unitOfWork.SaveChangesAsync();
                        Console.WriteLine($"--> Order updated!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not update order to Db {ex.Message}");
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
                case "OrderUpdate_Published":
                    Console.WriteLine("-->Order Update Published Event Detected");
                    return EventType.OrderUpdatePublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

    }
}
