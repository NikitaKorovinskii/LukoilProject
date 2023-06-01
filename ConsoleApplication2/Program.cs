using System;
using ClickHouse.Net;
using Confluent.Kafka;
using System.Data.SqlClient;


namespace ConsoleApplication2
{
    public class Listener
    {
        static void Main(string[] args)
        {
            //Создаем конфигурацию слушателя
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "my-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            
            // Подписываемся на топик
            consumer.Subscribe("my-topic");
            int i = 1;
            // Читаем сообщения из топика
            try
            {
                while (true)
                {
                    var result = consumer.Consume(TimeSpan.FromSeconds(1));
                    if (result != null)
                    {
                        var typeData = Convert.ToInt32(result.Message.Value);

                        var time = DateTime.Now;
                        Console.WriteLine(time);
                        string query = $"INSERT INTO dataMatricon (id, dataMatricons,time) VALUES ({i}, {typeData},'{time}');";
                        ConnectClickHouse db = new ConnectClickHouse();
                        db.openConnectClickHouse();
                        var x = db.getConnection();
                        using (var command = x.CreateCommand(query))
                        {
                            command.ExecuteNonQuery();
                        }
                        db.closeConnectClickHouse();
                        i++;
                        Console.WriteLine($"Получено сообщение: {result.Message.Value}");
                    }
                }
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Ошибка при получении сообщения: {e.Error.Reason}");
            }
            

            // // Создаем потребителя
            
        }
    }
}