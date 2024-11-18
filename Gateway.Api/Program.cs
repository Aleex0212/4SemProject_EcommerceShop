using System.Text;
using EcommerceShop.Common.Enum;
using EcommerceShop.Common.Policies;
using Gateway.Api.Auth;
using Gateway.Api.ServiceCollectionExtention;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

#region Dapr setup
var daprGrpcPort = Environment.GetEnvironmentVariable("DAPR_GRPC_PORT");
if (string.IsNullOrEmpty(daprGrpcPort))
{
  daprGrpcPort = "50001"; //altid sæt den til 50001
  Environment.SetEnvironmentVariable("DAPR_GRPC_PORT", daprGrpcPort);
}

var daprHttpPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");
if (string.IsNullOrEmpty(daprHttpPort))
{
  daprHttpPort = "5005"; //altid sæt den til 50001
  Environment.SetEnvironmentVariable("DAPR_HTTP_PORT", daprHttpPort);
}

// Add services to the container.
builder.Services.AddControllers()
  .AddDapr(config => config.UseGrpcEndpoint($"http://localhost:{daprGrpcPort}").UseHttpEndpoint($"http://localhost:{daprHttpPort}"));

builder.Services.AddDaprClient(config => config.UseGrpcEndpoint(daprGrpcPort).UseHttpEndpoint(daprHttpPort));
#endregion

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(o =>
  {
    o.RequireHttpsMetadata = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
      ValidIssuer = builder.Configuration["Jwt:Issuer"],
      ValidAudience = builder.Configuration["Jwt:Audience"],
      ClockSkew = TimeSpan.Zero
    };
  });

#region Policies
builder.Services.AddAuthorization(options => options
    .AddPolicy(UserPolicies.CustomerPolicy, policyBuilder => policyBuilder.RequireClaim(nameof(UserTypes), [UserTypes.Customer.ToString()])));
builder.Services.AddAuthorization(options => options
    .AddPolicy(UserPolicies.UserPolicy, policyBuilder => policyBuilder.RequireClaim(nameof(UserTypes), [UserTypes.User.ToString()])));
builder.Services.AddAuthorization(options => options
    .AddPolicy(UserPolicies.AdminPolicy, policyBuilder => policyBuilder.RequireClaim(nameof(UserTypes), [UserTypes.Admin.ToString()])));
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth();
builder.Services.AddScoped<LoginCustomer>();
builder.Services.AddSingleton<TokenProvider>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

#region dapr setup
app.UseCloudEvents(); //sørger for at medsendte parametre som DTO'er kan deserialiseres.
app.MapSubscribeHandler(); // kun ved explicit pubsub.

#endregion

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
