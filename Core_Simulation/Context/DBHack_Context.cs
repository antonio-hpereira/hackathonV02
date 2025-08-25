using API_Loan_Simulator.Entities;
using API_Loan_Simulator.Mapping;
using Microsoft.EntityFrameworkCore;
using System;

namespace API_Loan_Simulator.Context
{
    public class DBHack_Context: DbContext
    {       

        public DBHack_Context(DbContextOptions<DBHack_Context> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        //public DbSet<Simulacao> Simulacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMap()); 
        }
    }
}
