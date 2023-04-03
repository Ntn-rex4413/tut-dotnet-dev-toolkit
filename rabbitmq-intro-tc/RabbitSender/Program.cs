using RabbitMQ.Client;
using System.Text;

//for
//docker run -d --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management
ConnectionFactory factory = new();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

// name for this application
factory.ClientProvidedName = "Rabbit Sender App";

IConnection cnn = factory.CreateConnection();

IModel channel = cnn.CreateModel();

string exchangeName = "DemoExchange";

string routingKey = "demo-routing-key";

string queueName = "DemoQueue";



channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);

channel.QueueDeclare(queueName, false, false, false, null);

channel.QueueBind(queueName, exchangeName, routingKey, null);



for (int i = 0; i < 60; i++)
{
    Console.WriteLine($"Sending message {i}");
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Message #{i}");
    channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);
    Thread.Sleep(1000);
}



channel.Close();

cnn.Close();
