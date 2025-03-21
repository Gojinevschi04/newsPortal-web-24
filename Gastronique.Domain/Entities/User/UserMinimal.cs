﻿using System;
using Gastronique.Domain.Enums;

namespace Gastronique.Domain.Entities.User
{
     public class UserMinimal
     {
          public int Id { get; set; }
          public string Email { get; set; }
          public string Username { get; set; }
          public DateTime LastLogin { get; set; }
          public string LastIp { get; set; }
          public URole Level { get; set; }
     }
}