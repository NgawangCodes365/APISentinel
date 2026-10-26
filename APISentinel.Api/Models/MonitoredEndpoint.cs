namespace APISentinel.Api.Models;

public class MonitoredEndpoint
{
    public int id {get;set;}
    public string name{get;set;} = string.Empty;
    public string url{get;set;}=string.Empty;
    public int CheckIntervalSeconds{get;set;}=60;
    public bool IsEnabled{get;set;}=true;
    public DateTime CreatedAtUtc{get;set;}

}