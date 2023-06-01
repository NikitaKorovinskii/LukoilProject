using System;
using ClickHouse.Client.ADO;

namespace ConsoleApplication2
{
    public class ConnectClickHouse
    {
        ClickHouseConnection connection = new ClickHouseConnection("Host=localhost;Port=8123;Database=default;User=default;Password=;");
        public void openConnectClickHouse()
        {
            connection.Open();
            
        }
        public  void closeConnectClickHouse()
        {
            connection.Close();
        }

        public ClickHouseConnection getConnection()
        {
            return connection;
        }
    }
}