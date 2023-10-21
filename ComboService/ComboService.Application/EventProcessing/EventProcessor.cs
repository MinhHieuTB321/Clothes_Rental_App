using AutoMapper;
using ComboService.Application.ViewModels;
using ComboService.Application.ViewModels.PublishedModels;
using ComboService.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ComboService.Application.EventProcessing
{
	enum EventType
	{
		ShopPublished,
		ProductPublished,
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
				case EventType.ShopPublished:
					await AddShop(message);
					break;
				case EventType.ProductPublished:
					await AddProduct(message);
					break;
				default:
					break;
			}
		}

		private async Task AddProduct(string message)
		{
			using (var scope = _scopeFactory.CreateScope())
			{
				var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
				var productCreateModel = JsonSerializer.Deserialize<ProductCreateModel>(message);

				try
				{
					var product = _mapper.Map<Product>(productCreateModel);
					await unitOfWork.Repository<Product>().InsertAsync(product);
					await unitOfWork.CommitAsync();
					Console.WriteLine($"--> Product Added!");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"--> Could not update order to Db {ex.Message}");
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
					await unitOfWork.Repository<Shop>().InsertAsync(shop);
					await unitOfWork.CommitAsync();
					Console.WriteLine($"--> Shop Added!");

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
				case "Shop_Published":
					Console.WriteLine("-->Shop Published Event Detected");
					return EventType.ShopPublished;
				case "Product_Published":
					Console.WriteLine("-->Product Published Event Detected");
					return EventType.ProductPublished;
				default:
					Console.WriteLine("--> Could not determine the event type");
					return EventType.Undetermined;
			}
		}

	}
}
