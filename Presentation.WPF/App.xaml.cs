﻿using Business.Interfaces;
using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.WPF.ViewModels;
using Presentation.WPF.Views;
using Shared.Utils;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Presentation.WPF
{
    public partial class App : Application
    {
        private IHost builder;

        public App()
        {
            builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                Logger.LogFilePath = @"c:\Education\Datalagring-assignment\log.txt";

                services.AddDbContext<CustomerContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Education\Datalagring-assignment\Infrastructure\Data\customer_database.mdf;Integrated Security=True"));
                services.AddDbContext<ProductCatalogContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Education\Datalagring-assignment\Infrastructure\Data\productcatalog_database_df.mdf;Integrated Security=True"));

                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IRoleRepository, RoleRepository>();
                services.AddScoped<IAuthRepository, AuthRepository>();
                services.AddScoped<ICategoryRepository, CategoryRepository>();
                services.AddScoped<IProductRepository, ProductRepository>();
                services.AddScoped<IOrderRepository, OrderRepository>();
                services.AddScoped<IOrderService, OrderService>();
                services.AddScoped<IAuthService, AuthService>();
                services.AddScoped<IProductService, ProductService>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainOptionsViewModel>();
                services.AddSingleton<MainOptions>();

                services.AddTransient<SignUpViewModel>();
                services.AddTransient<SignUpView>();
                services.AddTransient<GetAllUserViewModel>();
                services.AddTransient<GetAllUserView>();
                services.AddTransient<GetOneUserViewModel>();
                services.AddTransient<GetOneUserView>();
                services.AddTransient<UpdateUserViewModel>();
                services.AddTransient<UpdateUserView>();
                services.AddTransient<DeleteUserViewModel>();
                services.AddTransient<DeleteUserView>();
                services.AddTransient<AddProductViewModel>();
                services.AddTransient<AddProductView>();
                services.AddTransient<MainOptionsProductViewModel>();
                services.AddTransient<MainOptionsProductView>();
                services.AddTransient<MainOptionsOrderViewModel>();
                services.AddTransient<MainOptionsOrderView>();
                services.AddTransient<AddOrderViewModel>();
                services.AddTransient<AddOrderView>();
                services.AddTransient<GetOneProductViewModel>();
                services.AddTransient<GetOneProductView>();
                services.AddTransient<GetOneOrderViewModel>();
                services.AddTransient<GetOneOrderView>();
            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            builder.Start();

            var mainWindow = builder.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
