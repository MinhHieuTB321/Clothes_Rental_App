﻿using AutoMapper;
using ComboService.Application.Interfaces;
using ComboService.Application.ViewModels.PublishedModels;
using ComboService.Application.ViewModels.Response;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ComboService.Application.Services
{
	public class MessageBusClient:IMessageBusClient
	{
		private readonly IConfiguration _config;
		private readonly IConnection? _connection;
		private readonly IModel _chanel;
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

		public void PublishedCombo(ComboResponseModel model)
		{
			var comboPubblishModel = _mapper.Map<ComboPublishedModel>(model);
			var message = JsonSerializer.Serialize(comboPubblishModel);

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

		public void UpdatedCombo(ComboResponseModel model)
		{
			var comboPubblishModel = _mapper.Map<ComboPublishedModel>(model);
			comboPubblishModel.Event = "Updated_Combo";
			var message = JsonSerializer.Serialize(comboPubblishModel);

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

		public void DeletedCombo(ComboResponseModel model)
		{
			var comboPubblishModel = _mapper.Map<ComboPublishedModel>(model);
			comboPubblishModel.Event = "Deleted_Combo";
			var message = JsonSerializer.Serialize(comboPubblishModel);

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
