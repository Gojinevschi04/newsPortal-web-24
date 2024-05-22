﻿using System;
using NewsPortal.Domain.Enums;

namespace NewsPortal.Domain.Entities.User
{
     public class UserMinimal
     {
          public int Id { get; set; }
          public string Email { get; set; }
          public DateTime LastLogin { get; set; }
          public string LasIp { get; set; }
          public URole Level { get; set; }
     }
}