using System;
using System.Text;
using System.Threading;
using EventBus.Abstractions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBus
{
    public class IntegrationEvent : IIntegrationEvent, IDisposable
    {
        public string Read()
        {
            string message = string.Empty;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"                
            };

            using(var conn = factory.CreateConnection())
            using(var channel = conn.CreateModel())
            {
                channel.QueueDeclare(queue: "price_queue",
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
                
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new EventingBasicConsumer(channel);
                                        
                consumer.Received += (model, ea) => 
                {
                    var body = ea.Body;
                    message = Encoding.UTF8.GetString(body);
                };

                channel.BasicConsume(queue: "price_queue",autoAck: false, consumer : consumer);

                return message;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Publish(string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            using(var conn = factory.CreateConnection())
            using(var channel = conn.CreateModel())
            {
                channel.QueueDeclare(queue: "price_queue",
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
                
                string msg = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(msg);

                var prop = channel.CreateBasicProperties();
                prop.Persistent = true;

                channel.BasicPublish(exchange: "",
                                        routingKey: "price_queue",
                                        basicProperties: prop,
                                        body: body);
            }
        }
        
    }
}