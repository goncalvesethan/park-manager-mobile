using System.Text.Json.Serialization;

namespace Models;

public class Incident
{
    public int Id { get; set; }
    public int ReporterId { get; set; }
    public int DeviceId { get; set; }
    public string Status { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    
    [JsonIgnore]
    public User? Reporter { get; set; }

    [JsonIgnore]
    public Device? Device { get; set; }

    public string StatusLabel => Status == "open" ? "En attente" : "Résolu";

}