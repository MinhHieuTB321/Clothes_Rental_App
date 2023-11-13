using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ShopService.Application.ViewModels;
using ShopService.Application.ViewModels.Owners;
using ShopService.Domain.Entities;
using System.Text.Json;


namespace ShopService.Application.EventProcessing
{
    enum EventType
    {
        OwnerPublished,
        OwnerUpdated,
        OwnerDeleted,
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
                case EventType.OwnerPublished:
                    await AddOwner(message);
                    break;
                case EventType.OwnerUpdated:
                    await UpdateOwner(message);
                    break;
                case EventType.OwnerDeleted:
                    await DeleteOwner(message);
                    break;
                default:
                    break;
            }
        }

        private async Task DeleteOwner(string message)
        {
            using(var scope= _scopeFactory.CreateScope())
            {
                var unitOfWork= scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var model = JsonSerializer.Deserialize<OwnerPublishedModel>(message);

                try
                {   
                    var user=await unitOfWork.OwnerRepository.GetByIdAsync(model!.Id);
                    if(user!=null)
                    {
                        unitOfWork.OwnerRepository.SoftRemove(user);
                        await unitOfWork.SaveChangeAsync();
                        Console.WriteLine($"--> owner Deleted!");
                    }
                }   
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not delete owner to Db {ex.Message}");
                }
            }
        }

        private async Task UpdateOwner(string message)
        {
            using(var scope= _scopeFactory.CreateScope())
            {
                var unitOfWork= scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var model = JsonSerializer.Deserialize<OwnerPublishedModel>(message);

                try
                {   
                    var user=await unitOfWork.OwnerRepository.GetByIdAsync(model!.Id);
                    if(user!=null)
                    {
                        user= _mapper.Map(model,user);
                        unitOfWork.OwnerRepository.Update(user);
                        await unitOfWork.SaveChangeAsync();
                        Console.WriteLine($"--> owner Updated!");
                    }
                }   
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not update owner to Db {ex.Message}");
                }
            }
        }

        private async Task AddOwner(string message)
        {
            using(var scope= _scopeFactory.CreateScope())
            {
                var unitOfWork= scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var model = JsonSerializer.Deserialize<OwnerPublishedModel>(message);

                try
                {   
                    var user= _mapper.Map<Owner>(model);
                    if(await unitOfWork.OwnerRepository.GetByIdAsync(user.Id) ==null)
                    {
                        await unitOfWork.OwnerRepository.AddAsync(user);
                        await unitOfWork.SaveChangeAsync();
                        Console.WriteLine($"--> owner added!");
                    }
                }   
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not add owner to Db {ex.Message}");
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
                case "Owner_Published":
                    Console.WriteLine("-->Owner Published Event Detected");
                    return EventType.OwnerPublished;
                case "Owner_Updated":
                    Console.WriteLine("-->Owner Updated Event Detected");
                    return EventType.OwnerUpdated;
                case "Owner_Deleted":
                    Console.WriteLine("-->Owner Deleted Event Detected");
                    return EventType.OwnerDeleted;
                default:
                    Console.WriteLine("--> No Event Type Detected!");
                    return EventType.Undetermined;
            }
        }

    }
}
