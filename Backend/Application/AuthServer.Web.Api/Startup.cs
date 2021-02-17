using AuthServer.Application.Mapping;
using AuthServer.Application.Roles.Commands;
using AuthServer.Application.Users.Models;
using AuthServer.EntityFrameworkCore;
using AuthServer.Persistence.Repository;
using AuthServer.Security;
using AuthServer.Web.Api.Filters;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace AuthServer.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ServerDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(Configuration.GetConnectionString("ServerDataBase"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration["Identity:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.Audience = Configuration["Identity:Audience"];
                });

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ApiResponseWrapperFilter));
                options.Filters.Add(typeof(ExceptionWrapperFilter));
            })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateRoleCommandValidator>());

            services.AddHttpContextAccessor();

            services.AddScoped<IRepository, Repository>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddMediatR(typeof(UserModel).Assembly);

            services.AddScoped<IAuthorizationHandler, APIAuthorizationHandler>();

            #region Filter
            services.AddScoped<ApiResponseWrapperFilter>();

            services.AddScoped<ExceptionWrapperFilter>();
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth Server API", Version = "v1", TermsOfService = null });
                c.IncludeXmlComments(Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "AuthServer.xml"), true);

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    OpenIdConnectUrl = new Uri($"{Configuration["Identity:Authority"]}{Configuration["Identity:ConnectUrl"]}"),
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{Configuration["Identity:Authority"]}{Configuration["Identity:AuthorizeUrl"]}"),
                            Scopes = { { Configuration["Identity:Scopes"], Configuration["Identity:ScopesDescription"] } }
                        }
                    }
                });

                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });
            services.AddSwaggerGenNewtonsoftSupport();
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth Server V1");
                c.RoutePrefix = string.Empty;
                c.OAuthClientId(Configuration["Identity:ClientId"]);
            });
        }
    }
}
