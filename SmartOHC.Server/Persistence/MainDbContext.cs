using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOHC.Server.Persistence
{
    public class MainDbContext: DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options): base(options)
        {

        }
        public DbSet<LogDataModel> Logs { get; set; }
    }
}
