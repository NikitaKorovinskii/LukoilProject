using System;
using System.Threading;
using ClickHouse.Net;
using Confluent.Kafka;
using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Apache kafka Продюсер
            TitaniumAS.Opc.Client.Bootstrap.Initialize();

            Uri url = UrlBuilder.Build("Matrikon.OPC.Simulation.1");
            using (var server = new OpcDaServer(url))
            {
                server.Connect();

                OpcDaGroup pressure = server.AddGroup("Pressure");
                pressure.IsActive = true;

                OpcDaGroup density = server.AddGroup("Density");
                density.IsActive = true;
                OpcDaGroup concentration = server.AddGroup("Concentration");
                concentration.IsActive = true;
                OpcDaGroup time = server.AddGroup("Time");
                concentration.IsActive = true;


                var itemFloat = new OpcDaItemDefinition
                {
                    ItemId = "Random.Int8",
                    IsActive = true
                };
             
                OpcDaItemDefinition[] opcDaItems_pressure = { itemFloat };
                OpcDaItemDefinition[] opcDaItems_density = { itemFloat };
                OpcDaItemDefinition[] opcDaItems_concentration = { itemFloat };

                OpcDaItemResult[] resultsPressureList = pressure.AddItems(opcDaItems_pressure);
                OpcDaItemResult[] resultsDensityList = density.AddItems(opcDaItems_density);
                OpcDaItemResult[] resultsConcentrationList = concentration.AddItems(opcDaItems_concentration);

                var config = new ProducerConfig
                {
                    BootstrapServers = "localhost:9092",
                    ClientId = "my-app"
                };

                // Создаем продюсера
                using var producer = new ProducerBuilder<Null, string>(config).Build();

                // Отправляем сообщение в Kafka
                Message<Null, string> messagePressure;
                Message<Null, string> messageDensity;
                Message<Null, string> messageConcentration;
               
                while (true)
                {
                    OpcDaItemValue[] valuesPressure = pressure.Read(pressure.Items, OpcDaDataSource.Device);
                    OpcDaItemValue[] valuesDensity = density.Read(density.Items, OpcDaDataSource.Device);
                    OpcDaItemValue[] valuesConcentration = concentration.Read(concentration.Items, OpcDaDataSource.Device);
                    messagePressure = new Message<Null, string> { Value = $"{Convert.ToString(valuesPressure[0].Value)}" };
                    messageDensity = new Message<Null, string> { Value = $"{Convert.ToString(valuesDensity[0].Value)}" };
                    messageConcentration = new Message<Null, string> { Value = $"{Convert.ToString(valuesConcentration[0].Value)}" };
                    Thread.Sleep(3000);
                    try
                    {
                        var result = producer.ProduceAsync("my-topic", messagePressure).GetAwaiter().GetResult();
                        result = producer.ProduceAsync("my-topic", messageDensity).GetAwaiter().GetResult();
                        result = producer.ProduceAsync("my-topic", messageConcentration).GetAwaiter().GetResult();
                        Console.WriteLine($"Сообщение отправлено в топик {result.Topic} по позиции {result.Offset}");
                    }
                    catch (ProduceException<Null, string> e)
                    {
                        Console.WriteLine($"Ошибка при отправке сообщения: {e.Error.Reason}");
                    }

                    //Ожидаем отправки всех сообщений
                    producer.Flush(TimeSpan.FromSeconds(10));
                }

                
                //Подключение к PostgreSQL
            //             using System.Collections.Generic;
            // using Npgsql;
            //
            // namespace ConsoleApplication1
            // {
            //     public class ConnectPostgres
            //     {
            //         private string connectionString = "Host=localhost;Port=5432;Database=pribors;Username=postgres;Password=11111;";
            //         private NpgsqlConnection _npgSqlConnection;
            //         
            //         public ConnectPostgres()
            //         {
            //             _npgSqlConnection = new NpgsqlConnection(connectionString);
            //             _npgSqlConnection.Open();
            //         }
            //         public List<string> GetPribors()
            //         {
            //             var command = new NpgsqlCommand("SELECT name FROM pribor",_npgSqlConnection);
            //             var list = new List<string>();
            //             
            //             using (var reader = command.ExecuteReader())
            //             {
            //                 if (reader.HasRows)
            //                 {
            //                     while (reader.Read())
            //                     {
            //                         string str = reader.GetString(reader.GetOrdinal("name"));
            //                         list.Add(str);
            //                     }
            //                 }
            //             }
            //             return list;
            //         }
            //         
            //     }
            // }

            }
        }
    }
}