using AutoMapper;
using ComboService.Application.ViewModels;
using ComboService.Application.ViewModels.PublishedModels;
using ComboService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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
		ProductPublished,
		ProductUpdated,
		ProductDeleted,
		ShopPublished,
		ShopUpdated,
		ShopDeleted,
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
				case EventType.ShopUpdated:
					await UpdateShop(message);
					break;
				case EventType.ShopDeleted:
					await DeleteShop(message);
					break;
				case EventType.ProductPublished:
					await AddProduct(message);
					break;
				case EventType.ProductUpdated:
					await UpdateProduct(message);
					break;
				case EventType.ProductDeleted:
					await DeleteProduct(message);
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
				var shopCreateModel = JsonConvert.DeserializeObject<ShopCreateModel>(message);

				try
				{
					var shop = await unitOfWork.Repository<Shop>().GetAll().FirstOrDefaultAsync(x => x.Id == shopCreateModel.Id && x.IsDeleted == false);
					unitOfWork.Repository<Shop>().SoftRemove(shop);
					await unitOfWork.CommitAsync();
					Console.WriteLine($"--> Shop Deleted!");

				}
				catch (Exception ex)
				{
					Console.WriteLine($"--> Could not Delete shop to Db {ex.Message}");
				}
			}
		}

		private async Task UpdateShop(string message)
		{
			using (var scope = _scopeFactory.CreateScope())
			{
				var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
				var shopCreateModel = JsonConvert.DeserializeObject<ShopCreateModel>(message);

				try
				{
					var shop = await unitOfWork.Repository<Shop>().GetAll().FirstOrDefaultAsync(x=>x.Id==shopCreateModel.Id && x.IsDeleted==false);
					shop = _mapper.Map(shopCreateModel,shop);
					await unitOfWork.Repository<Shop>().UpdateDetached(shop);
					await unitOfWork.CommitAsync();
					Console.WriteLine($"--> Shop Updated!");

				}
				catch (Exception ex)
				{
					Console.WriteLine($"--> Could not Update shop to Db {ex.Message}");
				}
			}
		}

		private async Task DeleteProduct(string message)
		{
			using (var scope = _scopeFactory.CreateScope())
			{
				var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
				var productCreateModel = JsonConvert.DeserializeObject<ProductCreateModel>(message);

				try
				{
					var product = await unitOfWork.Repository<Product>().GetAll().FirstOrDefaultAsync(x => x.Id == productCreateModel.Id);
					unitOfWork.Repository<Product>().SoftRemove(product);
					await unitOfWork.CommitAsync();
					Console.WriteLine($"--> Product Deleted!");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"--> Could not Delete product to Db {ex.Message}");
				}
			}
		}

		private async Task UpdateProduct(string message)
		{
			using (var scope = _scopeFactory.CreateScope())
			{
				var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
				var productCreateModel = JsonConvert.DeserializeObject<ProductCreateModel>(message);

				try
				{
					var product = await unitOfWork.Repository<Product>().GetAll().FirstOrDefaultAsync(x=>x.Id== productCreateModel.Id);
					product = _mapper.Map(productCreateModel,product);
					product.ProductImages = _mapper.Map(productCreateModel.ProductImages,product.ProductImages);
					await unitOfWork.Repository<Product>().UpdateDetached(product);
					await unitOfWork.CommitAsync();
					Console.WriteLine($"--> Product Updated!");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"--> Could not update product to Db {ex.Message}");
				}
			}
		}

		private async Task AddProduct(string message)
		{
			using (var scope = _scopeFactory.CreateScope())
			{
				var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
				var productCreateModel = JsonConvert.DeserializeObject<ProductCreateModel>(message);

				try
				{
					var product = _mapper.Map<Product>(productCreateModel);
					product.ProductImages= _mapper.Map<ICollection<ProductImage>>(productCreateModel.ProductImages);
					await unitOfWork.Repository<Product>().InsertAsync(product);
					//await unitOfWork.Repository<ProductImage>().InsertRangeAsync(images.AsQueryable());
					await unitOfWork.CommitAsync();
					Console.WriteLine($"--> Product Added!");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"--> Could not add product to Db {ex.Message}");
				}
			}
		}

		private async Task AddShop(string message)
		{
			using (var scope = _scopeFactory.CreateScope())
			{
				var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
				var shopCreateModel = JsonConvert.DeserializeObject<ShopCreateModel>(message);

				try
				{
					var shop = _mapper.Map<Shop>(shopCreateModel);
					await unitOfWork.Repository<Shop>().InsertAsync(shop);
					
					await unitOfWork.CommitAsync();
					Console.WriteLine($"--> Shop Added!");

				}
				catch (Exception ex)
				{
					Console.WriteLine($"--> Could not Add shop to Db {ex.Message}");
				}
			}
		}


		private EventType DetermineEvent(string notificationMessage)
		{
			Console.WriteLine("--> Determining Event");
			//Console.WriteLine(notificationMessage);
			var eventType = JsonConvert.DeserializeObject<GenericEventModel>(notificationMessage);
			//Console.WriteLine(eventType);
			switch (eventType!.Event)
			{
				case "Shop_Published":
					Console.WriteLine("-->Shop Published Event Detected");
					return EventType.ShopPublished;
				case "Shop_Updated":
					Console.WriteLine("-->Shop Updated Event Detected");
					return EventType.ShopUpdated;
				case "Shop_Deleted":
					Console.WriteLine("-->Shop Deleted Event Detected");
					return EventType.ShopDeleted;
				case "Product_Published":
					Console.WriteLine("-->Product Published Event Detected");
					return EventType.ProductPublished;
				case "Product_Updated":
					Console.WriteLine("-->Product Updated Event Detected");
					return EventType.ProductUpdated;
				case "Product_Deleted":
					Console.WriteLine("-->Product Deleted Event Detected");
					return EventType.ProductDeleted;
				default:
					Console.WriteLine("--> No Event Type Detected!");
					return EventType.Undetermined;
			}
		}

	}
}
