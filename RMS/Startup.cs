using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RMS.Authorization;
using RMS.Data;
using RMS.Helper;
using RMS.Services;
using System.Reflection;

namespace RMS
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
         var dbString = Configuration["ConnectionStrings:Mysql"];
         services.AddDbContext<RMSContext>(opt =>
            opt.UseMySQL(dbString));
         services.AddControllers();
         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "RMS", Version = "v1" });
            c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
               Type = SecuritySchemeType.Http,
               BearerFormat = "JWT",
               In = ParameterLocation.Header,
               Scheme = "bearer"
            });
            c.OperationFilter<AuthenticationRequirementsOperationFilter>();
         });

         services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
         services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
         services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials())
         );

         services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
         services.AddScoped<IJwtUtils, JwtUtils>();
         services.AddScoped<IUserService, UserService>();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         //if (env.IsDevelopment())
         //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RMS v1"));
         //}

         // Redirect 404 to frontend
         // https://www.infoworld.com/article/3545304/how-to-handle-404-errors-in-aspnet-core-mvc.html
         app.Use(async (context, next) =>
         {
            await next();
            if (context.Response.StatusCode == 404)
            {
               context.Request.Path = "/";
               await next();
            }
         });

         app.UseHttpsRedirection();
         app.UseCors("CorsPolicy");
         app.UseRouting();
         app.UseDefaultFiles();
         app.UseStaticFiles();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
