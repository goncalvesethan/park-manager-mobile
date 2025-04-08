using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Models;

public class User
{
    public required int Id { get; set; }
    public required string Lastname { get; set; }
    public required string Firstname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required bool IsAdmin { get; set; }
    
    [JsonIgnore]
    public string Name => $"{Firstname} {Lastname}";
}
