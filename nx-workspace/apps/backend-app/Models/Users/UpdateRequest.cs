namespace BackendApp.Models.Users;

using System.ComponentModel.DataAnnotations;
using BackendApp.Entities;

public class UpdateRequest
{
    public string Username { get; set; }
    public int Active { get; set; }
    
    private string _password;
    [MinLength(6)]
    public string Password
    {
        get => _password;
        set => _password = replaceEmptyWithNull(value);
    }

    private string _confirmPassword;
    [Compare("Password")]
    public string ConfirmPassword 
    {
        get => _confirmPassword;
        set => _confirmPassword = replaceEmptyWithNull(value);
    }

    private string replaceEmptyWithNull(string value)
    {
        // replace empty string with null to make field optional
        return string.IsNullOrEmpty(value) ? null : value;
    }
}