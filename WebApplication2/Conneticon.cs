using ClickHouse.Client.ADO;

namespace WebApplication2;


public class Conneticon
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