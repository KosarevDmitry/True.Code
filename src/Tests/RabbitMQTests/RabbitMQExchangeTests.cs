using System.Diagnostics;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using Xunit.Abstractions;
using True.Code.ToDoListAPI.Tests.TestHelper;
using Xunit.Sdk;

namespace True.Code.ToDoListAPI.Tests;

public class RabbitMQExchangeTests : TestBase
{
    public RabbitMQExchangeTests(ITestOutputHelper output) : base(output)
    {
    }


    [Theory]
    [InlineData("direct_logs", ExchangeType.Direct, false, 4)]
    [InlineData("logs",        ExchangeType.Fanout, true,  9)]
    [InlineData("topic_logs",  ExchangeType.Topic,  false, 5)]
    public async void Exchange(string exchange, string exchangeType, bool autoack, int expectednumber)
    {
        CountdownEvent countdown = new(expectednumber); // number of messages received
        if (exchangeType == ExchangeType.Direct || exchangeType == ExchangeType.Fanout)
        {
            Parallel.Invoke(
                () => _ = Task.Factory.StartNew(() => Receive("b1", new[] { "warning", "error" })),
                () => _ = Task.Factory.StartNew(() => Receive("b2", new[] { "warning" })),
                () => _ = Task.Factory.StartNew(() => Receive("b3", new[] { "info" }))
            );
            Thread.Sleep(1000);
            await Task.Factory.StartNew(() => Send("info",    "1"));
            await Task.Factory.StartNew(() => Send("warning", "2"));
            await Task.Factory.StartNew(() => Send("error",   "3"));
        }

        if (exchangeType == ExchangeType.Topic)
        {
            Parallel.Invoke(
                () => _ = Task.Factory.StartNew(() => Receive("b4", new[] { "#" })),
                () => _ = Task.Factory.StartNew(() => Receive("b5", new[] { "boo.#" })),
                () => _ = Task.Factory.StartNew(() => Receive("b6", new[] { "#.foo" }))
            );

            Thread.Sleep(1000);
            await Task.Factory.StartNew(() => Send("boo.foo", "4"));
            await Task.Factory.StartNew(() => Send("bar.baz", "5"));
            await Task.Factory.StartNew(() => Send("#",       "6"));
        }

        void Receive(string name, string[] severities)
        {
            var       factory    = new ConnectionFactory();
            using var connection = factory.CreateConnection();
            using var channel    = connection.CreateModel();

            channel.ExchangeDeclare(exchange: exchange, type: exchangeType);

            var queueName = channel.QueueDeclare().QueueName;


            foreach (var severity in severities)
            {
                channel.QueueBind(queue: queueName,
                    exchange: exchange,
                    routingKey: severity);
            }

            var    consumer = new EventingBasicConsumer(channel);
            string message  = default;
            string routingKey;
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                message    = Encoding.UTF8.GetString(body);
                routingKey = ea.RoutingKey;
                $"{name} received {routingKey}: {message}".Dump();
                //  Interlocked.Decrement(ref actual);
                countdown.Signal();
            };

            channel.BasicConsume(queue: queueName,
                autoAck: autoack,
                consumer: consumer);

            int     ms      = 10;
              TimeSpan timeout = TimeSpan.FromMilliseconds(ms);
              //Exit  by timeout is considered an error
              Assert.True(countdown.Wait(timeout), $"Fewer messages than expected {expectednumber}  received in {ms} milliseconds");

        }

        void Send(string routingKey, string message)
        {
            var       factory    = new ConnectionFactory();
            using var connection = factory.CreateConnection();
            using var channel    = connection.CreateModel();
            channel.ExchangeDeclare(exchange: exchange, type: exchangeType);

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: exchange,
                routingKey: routingKey,
                basicProperties: null,
                body: body);
            $"Sent {routingKey}:{message}".Dump();
        }
    }
}
