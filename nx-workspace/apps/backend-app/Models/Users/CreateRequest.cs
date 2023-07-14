namespace BackendApp.Models.Users;

using System.ComponentModel.DataAnnotations;
using BackendApp.Entities;

public class CreateRequest
{
    [Required]
    [MinLength(6)]
    public string Username { get; set; }

    [Required]
    public int Active { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}