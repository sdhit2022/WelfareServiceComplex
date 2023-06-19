﻿using Application.Common;
using Application.Interfaces.Context;
using Application.Product;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ServiceComplex;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .CreateLogger();

Log.Information("شروع راه اندازی");
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog().ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Trace);
    });
    var configuration = builder.Configuration;
    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

    builder.Services.AddRazorPages().AddMvcOptions(x => x.Filters.Add<Security>());

    #region IOC

    builder.Services.AddTransient<IComplexContext, ComplexContext>();
    builder.Services.AddTransient<ISaleInContext, SaleInContext>();
    builder.Services.AddSession();
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddFluentValidationClientsideAdapters();
    RegisterServices.Configure(builder.Services);

    #endregion


    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromSeconds(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    #region connection string

    var saleInConnection = configuration.GetConnectionString("SaleInConnection");
    var ShopConnection = configuration.GetConnectionString("ComplexConnection1"); // for use branch remove this

    builder.Services.AddDbContext<SaleInContext>(option => option.UseSqlServer(saleInConnection));

    builder.Services.AddDbContext<ComplexContext>((serviceProvider, options) =>
    {
        var httpContext = serviceProvider.GetService<IHttpContextAccessor>().HttpContext;

        if (httpContext == null) return;
        string session = null;


        try
        {
            session = httpContext.Session.GetConnectionString("Branch"); //branch connection
            options.UseSqlServer(session);
        }
        catch (Exception exception)
        {
            Log.Error($"ارتباط با سرور شبعه مورد نظر برقرار نشد  {exception}");
            throw new ConnectionAbortedException($"Can't Connect to this Connection {session} because {exception}");
        }
    });

    #endregion

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseSession();
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "اجرایی اپلیکشن با خطای زیر مواجه شد.");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}