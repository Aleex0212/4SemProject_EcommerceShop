var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
return;

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
  app.UseRouting();
  app.UseAuthorization();

  app.UseEndpoints(endpoints =>
  {
    endpoints.MapHub<StatusUpdateHub>("/statusUpdateHub");
    endpoints.MapControllers();
  });
}
