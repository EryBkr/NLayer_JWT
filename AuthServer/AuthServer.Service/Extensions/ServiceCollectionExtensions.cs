using AuthServer.Core.Entities;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using AuthServer.Data;
using AuthServer.Data.Repositories;
using AuthServer.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthServer.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //Scoped Aynı Requestte bir defa oluşur
        //Transiet her seferinde yeni bir instance oluşturur
        //Singleton yaşam döngüsü boyunca aynı instance ile çalışır
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceDescriptors, string connectionString) 
        {
            //Context sınıfımı ekledim
            serviceDescriptors.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));
            serviceDescriptors.AddScoped<IAuthenticationService, AuthenticationService>();
            serviceDescriptors.AddScoped<IUserService, UserService>();
            serviceDescriptors.AddScoped<ITokenService, TokenService>();
            //Generic halleriyle entity den bağımsız Inject Ettim
            serviceDescriptors.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //iki adet Generic Class aldığımız için virgülle belirttik
            serviceDescriptors.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
            serviceDescriptors.AddScoped<IUnitOfWork, UnitOfWork>();

            //Identity Özelliklerini de belirledim
            serviceDescriptors.AddIdentity<User, IdentityRole>(Opt =>
            {
                Opt.User.RequireUniqueEmail = true;
                Opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            return serviceDescriptors;
        }
    }
}
