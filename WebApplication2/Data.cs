namespace WebApplication2;

public class Data
{
    public  Data() { }

    public Data(int pressure, int density, int concentration,string time)
    {
        this.pressure = pressure ;
        this.density = density ;
        this.concentration = concentration;
        this.time = time;

    }
    
    public int pressure;
    public int density;
    public int concentration;
    public string time;
}