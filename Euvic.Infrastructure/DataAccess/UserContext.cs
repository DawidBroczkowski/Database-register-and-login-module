﻿using Euvic.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Euvic.Infrastructure.DataAccess
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
