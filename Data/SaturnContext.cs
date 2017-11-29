using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using saturnUpload;

    public class SaturnContext : DbContext
    {
        public SaturnContext (DbContextOptions<SaturnContext> options)
            : base(options)
        {
        }

        public DbSet<saturnUpload.Diretorio> Diretorio { get; set; }

        public DbSet<saturnUpload.Arquivo> Arquivo { get; set; }
    }
