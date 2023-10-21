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
                default:
                    break;
            }
        }

        private async Task AddOwner(string message)
        {
            using(var scope= _scopeFactory.CreateScope())
            {
                var unitOfWork= scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var customerPublishedModel = JsonSerializer.Deserialize<OwnerPublishedModel>(message);

                try
                {   
                    var user= _mapper.Map<Owner>(customerPublishedModel);
                    if(await unitOfWork.OwnerRepository.GetByIdAsync(user.Id) ==null)
                    {
                        await unitOfWork.OwnerRepository.AddAsync(user);
                        await unitOfWork.SaveChangeAsync();
                        Console.WriteLine($"--> owner added!");
                    }
                }   
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not add user to Db {ex.Message}");
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
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

    }
}
