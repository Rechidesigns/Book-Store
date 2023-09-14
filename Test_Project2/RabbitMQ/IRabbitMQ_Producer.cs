namespace Test_Project2.RabbitMQ
{
    public interface IRabbitMQ_Producer
    {
        public void SendBookMessage<T>(T message);
    }
}
