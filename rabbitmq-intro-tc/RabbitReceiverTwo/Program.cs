using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

// name for this application
factory.ClientProvidedName = "Rabbit ReceiverTwo App";

IConnection cnn = factory.CreateConnection();

IModel channel = cnn.CreateModel();

string exchangeName = "DemoExchange";

string routingKey = "demo-routing-key";

string queueName = "DemoQueue";



channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);

channel.QueueDeclare(queueName, false, false, false, null);

channel.QueueBind(queueName, exchangeName, routingKey, null);

// one message processed at a time
channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (sender, args) =>
{
    Task.Delay(TimeSpan.FromSeconds(3)).Wait();

    var body = args.Body.ToArray();

    string message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Message received: {message}");

    channel.BasicAck(args.DeliveryTag, false);
};

string consumerTag = channel.BasicConsume(queueName, false, consumer);

// leaves the program running and listening for messages, like a running server
Console.ReadLine();

channel.BasicCancel(consumerTag);

channel.Close();

cnn.Close();
