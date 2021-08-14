using AuthServer.Core.Configuration;
using AuthServer.Core.Entities;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using AuthServer.Data;
using AuthServer.Data.Repositories;
using AuthServer.Service.Extensions;
using AuthServer.Service.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.API
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
            //Options Pattern
            //Token ayarlarýný modele çevirecek DI 'ý oluþturduk
            services.Configure<CustomTokenOptions>(Configuration.GetSection("TokenOptions"));

            //JWT ayarlarýnda tanýmlayacaðým için IOptions pattern mantýðýnda ki gibi Modelime appsettings te ki datamý bind ettim
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<CustomTokenOptions>();

            //Client ayarlarýný modele çevirecek DI 'ý oluþturduk
            services.Configure<List<Client>>(Configuration.GetSection("Clients"));

            //DI Registers
            services.LoadMyServices(Configuration.GetConnectionString("MyDbString"));

            //JWT Bearer ý ekliyorum
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //Þema Ýsmi belirlendi
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //JWT þemasý ile baðladým.Onu aþaðýda tanýmladým
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                //Jwt özelliklerini belirliyorum
                opt.TokenValidationParameters = new TokenValidationParameters 
                {
                    //Key i verdik
                    IssuerSigningKey=SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
                    ValidIssuer= tokenOptions.Issuer,
                    ValidAudience=tokenOptions.Audience[0],//Buna istek yapabilmesi için buna ait auidence bilgisinin tokene verilmiþ olmasý gerekir.Sadece buna olanýnýn verilmiþ olmasý yeterli olduðu için diðer audience bilgilerini vermedik hepsinide ekleyebilirdik
                    ValidateIssuerSigningKey=true,//Ýmza doðrulanmalý
                    ValidateAudience=true,//Audience kontrol edilsin
                    ValidateIssuer=true,//Issuer  i de doðrula
                    ValidateLifetime=true,//Yaþam ömrü de kontrol edilsin
                    ClockSkew=TimeSpan.Zero //Sunucu Time Zone farklýlýðýndan dolayý default olarak 5 dk ekliyor son zamana.Bunu iptal ediyoruz
                };
            });
            
            
            //Fluent Validation u projeye ekledim
            services.AddControllers().AddFluentValidation(opt=> 
            {
                //Bu projedeki bütün AbstractValidator larý bulup ekledik
                opt.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            //Fluent Validation da dönen hata mesajlarýný istediðimiz formatta sunuyoruz
            services.UseCustomValidationResponse();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthServer.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthServer.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); //Kimlik
            app.UseAuthorization(); //Yetkilendirme

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
