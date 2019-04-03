using Appzr.Domain.Commands;
using Appzr.Domain.Contracts;
using Appzr.Domain.Entities;
using Appzr.Domain.Handlers;
using Appzr.Domain.Helpers;
using Appzr.Domain.Messages;
using Appzr.Domain.Queries;
using Appzr.Infrastructure.Bus;
using Appzr.Infrastructure.LiteDB;
using Appzr.Infrastructure.Repositories;
using Appzr.Infrastructure.Repositories.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Appzr.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddSingleton<IMessageBroker, MessageBroker>();

            services.AddTransient<CqrsHelper>();
            services.AddTransient<MyAppHandler>();

            services.AddTransient<AddMyAppCommand>();
            services.AddTransient<ListMyAppsQuery>();
            services.AddTransient<SynchronizeMyAppCommand>();

            services.AddTransient<IRepository<MyAppEntity>, MyAppRepository>();
            services.AddTransient<IRepository<CommandEntity>, CommandRepository>();
            services.AddTransient<IQueryRepository<MyAppEntity>, MyAppQueryRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(configs =>
            {
                configs.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Exemplo - CQRS"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(configs =>
            {
                configs.SwaggerEndpoint("/swagger/v1/swagger.json", "API Docs - v1");
                configs.RoutePrefix = string.Empty;
            });

            LiteDBMapper.RegisterMaps();

            var bus = app.ApplicationServices.GetRequiredService<IMessageBroker>();
            var myAppHandler = app.ApplicationServices.GetRequiredService<MyAppHandler>();
            bus.ReceiveMessage(async message =>
            {
                switch (message)
                {
                    case MyAppAddedMessage myAppAddedMessage:
                        var cmd = app.ApplicationServices.GetRequiredService<SynchronizeMyAppCommand>();
                        cmd.Id = myAppAddedMessage.Id;
                        cmd.Name = myAppAddedMessage.Name;
                        cmd.Link = myAppAddedMessage.Link;
                        cmd.CreatedAt = myAppAddedMessage.CreatedAt;

                        await myAppHandler.Handle(cmd);
                        break;
                        
                    default:
                        break;
                }
            });
        }
    }
}
