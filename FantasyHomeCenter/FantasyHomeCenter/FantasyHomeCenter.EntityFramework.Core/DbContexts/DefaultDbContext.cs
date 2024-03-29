﻿using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FantasyHomeCenter.EntityFramework.Core
{
    [AppDbContext("DefaultConnection", DbProvider.Sqlite)]
    public class DefaultDbContext : AppDbContext<DefaultDbContext>
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }
    }
}