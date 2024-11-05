using OrderService.Api.ServiceCollectionExtensions;

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
  .AddDapr(config => config.UseGrpcEndpoint(daprGrpcPort).UseHttpEndpoint(daprHttpPort));
#endregion



//Service Registration
builder.Services.ServiceRegistration(builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDaprClient(config => config.UseGrpcEndpoint(daprGrpcPort).UseHttpEndpoint(daprHttpPort));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

#region dapr setup
app.UseCloudEvents(); //sørger for at medsendte parametre som DTO'er kan deserialiseres.
app.MapSubscribeHandler(); // kun ved explicit pubsub.

#endregion
app.MapControllers();

app.Run();
