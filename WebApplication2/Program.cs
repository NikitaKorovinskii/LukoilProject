using System.Text.Json;
using System.Text.Json.Serialization;
using WebApplication2;
using Newtonsoft.Json;
using ClickHouse.Net;
using System;
using Confluent.Kafka;
using System.Collections.Generic;


var builder = WebApplication.CreateBuilder();

builder.Services.AddCors(); // добавляем сервисы CORS
 
var app = builder.Build();

// настраиваем CORS
app.UseCors(builder => builder.AllowAnyOrigin());

List<Data> data = new List<Data>();
List<int> dataMatricon = new List<int>();

var query = "select * from dataMatricon;";

Conneticon db = new Conneticon();

db.openConnectClickHouse();

var x = db.getConnection();

using (var command = x.CreateCommand(query))
{
    using (var reader = command.ExecuteReader())
    {
        int i = 0;
        while (reader.Read())
        {
            i++;
            int column2 = reader.GetInt32(1);
            dataMatricon.Add(column2);
        }
    }
}
db.closeConnectClickHouse();

int j = 1;
for (int i = 0; i < dataMatricon.Count; i++)
{
    if (i < dataMatricon.Count - 2)
    {
        data.Add(new Data
        {
            density = dataMatricon[i] + j,
            concentration = dataMatricon[i+1] + j,
            pressure = dataMatricon[i+2] + j,
            time = DateTime.Now.AddMinutes(j).ToString()
        });
        j++;
    }
}
    


var json = JsonConvert.SerializeObject(data);



app.MapGet("/api", async context => await context.Response.WriteAsync(json));
app.Run();

