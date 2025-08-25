
using API_Loan_Simulator.Common;
using API_Loan_Simulator.Context;
using API_Loan_Simulator.Core_Simulator;
using API_Loan_Simulator.Core_Simulator.Estrategy.Concrete;
using API_Loan_Simulator.Core_Simulator.Estrategy.IEstrategy;
using API_Loan_Simulator.Core_Simulator.Factory;
using API_Loan_Simulator.Core_Simulator.ISimulator;
using API_Loan_Simulator.Core_Simulator.Service;
using API_Loan_Simulator.EventHub;
using API_Loan_Simulator.EventHub.IEventHub;
using API_Loan_Simulator.Repository.Concrete;
using API_Loan_Simulator.Repository.IRepository;
using API_Loan_Simulator.Telemetria;
using Core_Simulation.Repository.Concrete;
using Core_Simulation.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;

namespace API_Loan_Simulator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configura o DbContext com SQL Server
            builder.Services.AddDbContext<DBHack_Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConectionString")));

            builder.Services.AddDbContext<MemoryContext>(options =>
             options.UseInMemoryDatabase("BancoMemoria"));


            builder.Services.AddScoped<ISimuladorFinanciamento, SimuladorFinanciamento>();
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<IListaSimulacaoRepository, ListaSimulacaoRepository>();
            builder.Services.AddScoped<ISimuladorParcelasRepository, SimuladorParcelasRepository>();
            builder.Services.AddScoped<ISimulatorEventProducer, SimulacaoEventProducer>();
            builder.Services.AddScoped<IPersistSimulation, PersistSimulation>();
            builder.Services.AddScoped<IPersistSimutionRepository, PersistSimutionRepository>();
            builder.Services.AddScoped<ICalculoParcelasStrategy, CalculoPriceStrategy>();
            builder.Services.AddScoped<ICalculoParcelasStrategy, CalculoSACStrategy>();
            builder.Services.AddScoped<CalculoSACStrategy>();
            builder.Services.AddScoped<CalculoPriceStrategy>();
            builder.Services.AddScoped<ICalculoParcelasFactory, CalculoParcelasFactory>();
            builder.Services.AddScoped<SimuladorFinanciamentoService>();
            builder.Services.AddSingleton<ISimulacaoEnvioRepository, SimulacaoEnvioRepositoryEmMemoria>();





            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<TelemetriaStorage>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseMiddleware<TelemetriaMiddleware>();

            app.MapGet("/api/telemetria", (TelemetriaStorage storage) =>
            {
                var resumo = storage.ObterResumo();
                return Results.Ok(resumo);
            });


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
