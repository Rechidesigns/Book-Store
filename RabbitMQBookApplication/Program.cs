//Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
using RabbitMQ.Client.Events;
using RabbitMQ.Client;

var factory = new ConnectionFactory
{
    HostName = "localhost", Port = 7003,
    //Uri = new Uri($"amqp://gennera:gennera@{rabbitMqUrl}/"),
    DispatchConsumersAsync = false,
    ConsumerDispatchConcurrency = 1,
};

//Create the RabbitMQ connection using connection factory details as i mentioned above
var connection = factory.CreateConnection();
//Here we create channel with session and model
using
var channel = connection.CreateModel();
//declare the queue after mentioning name and a few property related to that
channel.QueueDeclare("book", exclusive: false);
//Set Event object which listen message from chanel which is sent by producer
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) => {
    var body = eventArgs.Body.ToArray();
    var message = System.Text.Encoding.UTF8.GetString(body);
    Console.WriteLine($"Book message received: {message}");
};
//read the message
channel.BasicConsume(queue: "book", autoAck: true, consumer: consumer);
Console.ReadKey();