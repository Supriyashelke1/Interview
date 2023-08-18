using System;
using System.Collections.Generic;

namespace Interview.Data;

public partial class UserLogin
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public string Role { get; set; } = null!;
}
