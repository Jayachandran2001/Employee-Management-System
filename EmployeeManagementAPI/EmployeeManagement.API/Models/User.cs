using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.Models;

public partial class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsActive { get; set; }
}
