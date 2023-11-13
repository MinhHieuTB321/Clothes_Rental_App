using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using ShopService.Application.Interfaces;
using ShopService.Application.ViewModels.Owners;
using ShopService.Application.ViewModels.Products;
using ShopService.Application.ViewModels.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopService.Application.Services
{
    public class MessageBusClient:IMessageBusClient
    {
        private readonly IConfiguration _config;
        private readonly IConnection? _connection;
        private readonly IModel? _chanel;
        private readonly IMapper _mapper;

        public MessageBusClient(IConfiguration configuration, IMapper mapper)
        {
            _config = configuration;
            _mapper = mapper;
            var factory = new ConnectionFactory()
            {
                HostName = _config["RabbitMQHost"],
                Port = int.Parse(_config["RabbitMQPort"]!)
            };
            try
            {
                _connection = factory.CreateConnection();
                _chanel = _connection.CreateModel();
                _chanel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("--> Connection to MessageBus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"-->Could not connect to the Message Bus:{ex.Message}");
            }
        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"--> Rabbit MQ Connection Shutdown");
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _chanel.BasicPublish(exchange: "trigger",
                            routingKey: "",
                            basicProperties: null,
                            body);
            Console.WriteLine($"--> We have sennt {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("MessageBus Disposed");
            if (_chanel!.IsOpen)
            {
                _chanel.Close();
                _connection!.Close();
            }
        }


        public void PublishedNewShop(ShopReadModel model)
        {
            var message = JsonConvert.SerializeObject(_mapper.Map<ShopPublishedModel>(model));

            if (_connection!.IsOpen)
            {
                Console.WriteLine("RabbitMQ Connection Open, Sending message...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("RabbitMQ Connection Closed, Not sending message...");
            }
        }

        public void PublishedNewProduct(ProductReadModel model)
        {
            var message = JsonConvert.SerializeObject(_mapper.Map<ProductPublishedModel>(model));

            if (_connection!.IsOpen)
            {
                Console.WriteLine("RabbitMQ Connection Open, Sending message...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("RabbitMQ Connection Closed, Not sending message...");
            }
        }

        public void UpdatedShop(ShopReadModel model)
        {
            var publishedModel=_mapper.Map<ShopPublishedModel>(model);
            publishedModel.Event="Shop_Updated";
            var message = JsonConvert.SerializeObject(publishedModel);

            if (_connection!.IsOpen)
            {
                Console.WriteLine("RabbitMQ Connection Open, Sending message...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("RabbitMQ Connection Closed, Not sending message...");
            }
        }

        public void DeletedShop(ShopReadModel model)
        {
             var publishedModel=_mapper.Map<ShopPublishedModel>(model);
            publishedModel.Event="Shop_Deleted";
            var message = JsonConvert.SerializeObject(publishedModel);

            if (_connection!.IsOpen)
            {
                Console.WriteLine("RabbitMQ Connection Open, Sending message...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("RabbitMQ Connection Closed, Not sending message...");
            }
        }

        public void UpdatedProduct(ProductReadModel model)
        {
            var publishedModel=_mapper.Map<ProductPublishedModel>(model);
            publishedModel.Event="Product_Updated";
             var message = JsonConvert.SerializeObject(publishedModel);

            if (_connection!.IsOpen)
            {
                Console.WriteLine("RabbitMQ Connection Open, Sending message...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("RabbitMQ Connection Closed, Not sending message...");
            }
        }

        public void DeletedProduct(ProductReadModel model)
        {
            var publishedModel=_mapper.Map<ProductPublishedModel>(model);
            publishedModel.Event="Product_Deleted";
             var message = JsonConvert.SerializeObject(publishedModel);

            if (_connection!.IsOpen)
            {
                Console.WriteLine("RabbitMQ Connection Open, Sending message...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("RabbitMQ Connection Closed, Not sending message...");
            }
        }
    }
}
