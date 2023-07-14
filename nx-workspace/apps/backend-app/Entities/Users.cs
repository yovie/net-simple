namespace BackendApp.Entities;

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

public class Users
{
    [Key]
    public string Username { get; set; }
    public int Active { get; set; }

    [JsonIgnore]
    public string Password { get; set; }
}