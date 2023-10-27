using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.ViewModels;
using OrderService.Application.ViewModels.Combos;
using OrderService.Application.ViewModels.Customers;
using OrderService.Application.ViewModels.Orders;
using OrderService.Application.ViewModels.Shops;
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
        ShopPublished,
        ShopUpdated,
        ShopDeleted,
        CustomerPublished,
        ComboPublished,
        ComboUpdated,
        ComboDeleted,
        CustomerUpdated,
        CustomerDeleted,
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
                case EventType.ShopPublished:
                    await AddShop(message);
                    break;
                case EventType.ShopUpdated:
                    await UpdateShop(message);
                    break;
                case EventType.ShopDeleted:
                    await DeleteShop(message);
                    break;
                case EventType.CustomerPublished:
                    await AddCustomer(message);
                    break;
                case EventType.CustomerUpdated:
                    await UpdateCustomer(message);
                    break;
                case EventType.CustomerDeleted:
                    await DeleteCustomer(message);
                    break;
                case EventType.ComboPublished:
                    await AddCombo(message);
                    break;
                case EventType.ComboUpdated:
                    await UpdateCombo(message);
                    break;
                case EventType.ComboDeleted:
                    await DeleteCombo(message);
                    break;
                default:
                    break;
            }
        }

        private async Task DeleteShop(string message)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var model = JsonSerializer.Deserialize<ShopCreateModel>(message);

                try
                {
                    var shop= await unitOfWork.ShopRepository.GetByIdAsync(model!.Id);
                    if(shop!=null){
                        unitOfWork.ShopRepository.SoftRemove(shop);
                        await unitOfWork.SaveChangesAsync();
                        Console.WriteLine($"--> Shop Deleted!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not delete shop to Db {ex.Message}");
                }
            }
        }

        private async Task UpdateShop(string message)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var model = JsonSerializer.Deserialize<ShopCreateModel>(message);

                try
                {
                    var shop= await unitOfWork.ShopRepository.GetByIdAsync(model!.Id);
                    if(shop!=null){
                        shop = _mapper.Map<Shop>(model);
                        unitOfWork.ShopRepository.Update(shop);
                        await unitOfWork.SaveChangesAsync();
                        Console.WriteLine($"--> Shop Updated!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not update shop to Db {ex.Message}");
                }
            }
        }

        private async Task DeleteCustomer(string message)
        {
            using(var scope= _scopeFactory.CreateScope())
            {
                var unitOfWork= scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var model = JsonSerializer.Deserialize<CustomerPublishedModel>(message);

                try
                {   
                    var user= await unitOfWork.CustomerRepository.GetByIdAsync(model!.Id);
                    if(user!=null)
                    {
                        unitOfWork.CustomerRepository.SoftRemove(user);
                        await unitOfWork.SaveChangesAsync();
                        Console.WriteLine($"--> customer Deleted!");
                    }
                }   
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not delete customer to Db {ex.Message}");
                }
            }
        }

        private async Task UpdateCustomer(string message)
        {
            using(var scope= _scopeFactory.CreateScope())
            {
                var unitOfWork= scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var model = JsonSerializer.Deserialize<CustomerPublishedModel>(message);

                try
                {   
                    var user= await unitOfWork.CustomerRepository.GetByIdAsync(model!.Id);
                    if(user!=null)
                    {
                        user= _mapper.Map(model,user);
                        unitOfWork.CustomerRepository.Update(user);
                        await unitOfWork.SaveChangesAsync();
                        Console.WriteLine($"--> customer Updated!");
                    }
                }   
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not update customer to Db {ex.Message}");
                }
            }
        }

        private async Task DeleteCombo(string message)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var comboCreateModel = JsonSerializer.Deserialize<ComboCreateModel>(message);

                try
                {
                    var combo = await unitOfWork.ComboRepository.GetByIdAsync(comboCreateModel!.Id);
                    unitOfWork.ComboRepository.SoftRemove(combo!);
                    await unitOfWork.SaveChangesAsync();
                    Console.WriteLine($"--> Combo Deleted!");
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not delete combo to Db {ex.Message}");
                }
            }
        }

        private async Task UpdateCombo(string message)
        {
           using (var scope = _scopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var comboCreateModel = JsonSerializer.Deserialize<ComboCreateModel>(message);

                try
                {
                    var combo = await unitOfWork.ComboRepository.GetByIdAsync(comboCreateModel!.Id);
                    combo = _mapper.Map(comboCreateModel,combo);
                    unitOfWork.ComboRepository.Update(combo!);
                    await unitOfWork.SaveChangesAsync();
                    Console.WriteLine($"--> Combo Updated!");
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not update combo to Db {ex.Message}");
                }
            }
        }

        private async Task AddCombo(string message)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var comboCreateModel = JsonSerializer.Deserialize<ComboCreateModel>(message);

                try
                {
                    var combo = _mapper.Map<Combo>(comboCreateModel);
                    await unitOfWork.ComboRepository.AddAsync(combo);
                    await unitOfWork.SaveChangesAsync();
                    Console.WriteLine($"--> Combo Added!");
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add new combo to Db {ex.Message}");
                }
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
                    var user= _mapper.Map<Customer>(customerPublishedModel);
                    if(await unitOfWork.CustomerRepository.GetByIdAsync(user.Id) ==null)
                    {
                        await unitOfWork.CustomerRepository.AddAsync(user);
                        await unitOfWork.SaveChangesAsync();
                        Console.WriteLine($"--> customer added!");
                    }
                }   
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not add customer to Db {ex.Message}");
                }
            }
        }

        private async Task AddShop(string message)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var shopCreateModel = JsonSerializer.Deserialize<ShopCreateModel>(message);

                try
                {
                    var shop = _mapper.Map<Shop>(shopCreateModel);
                    await unitOfWork.ShopRepository.AddAsync(shop);
                    await unitOfWork.SaveChangesAsync();
                    Console.WriteLine($"--> Shop Added!");
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not Add shop to Db {ex.Message}");
                }
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
                case "Combo_Published":
                    Console.WriteLine("-->Combo Published Event Detected");
                    return EventType.ComboPublished;
                case "Updated_Combo":
                    Console.WriteLine("-->Updated Combo Event Detected");
                    return EventType.ComboPublished;
                case "Deleted_Combo":
                    Console.WriteLine("-->Deleted Combo Event Detected");
                    return EventType.ComboPublished;
                case "Shop_Published":
                    Console.WriteLine("-->Shop Published Event Detected");
                    return EventType.ShopPublished;
                case "Shop_Updated":
                    Console.WriteLine("-->Shop Updated Event Detected");
                    return EventType.ShopUpdated;
                case "Shop_Deleted":
                    Console.WriteLine("-->Shop Deleted Event Detected");
                    return EventType.ShopDeleted;
                case "Customer_Published":
                    Console.WriteLine("-->Customer Published Event Detected");
                    return EventType.CustomerPublished;
                case "Customer_Updated":
                    Console.WriteLine("-->Customer Updated Event Detected");
                    return EventType.CustomerUpdated;
                case "Customer_Deleted":
                    Console.WriteLine("-->Customer Deleted Event Detected");
                    return EventType.CustomerDeleted;
                default:
                    Console.WriteLine("--> No Event Type Detected!");
                    return EventType.Undetermined;
            }
        }

    }
}
