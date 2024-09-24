var builder = WebApplication.CreateBuilder(args);

var allowedHosts = builder.Configuration.GetValue<string>("AllowedHosts");
var clientBaseUrl = builder.Configuration.GetValue<string>("ApiSettings:ClientBaseUrl");
var hostUrl = builder.Configuration.GetValue<string>("ApiSettings:HostUrl");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        corsBuilder => corsBuilder
            .WithOrigins(clientBaseUrl)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.WebHost.UseUrls(hostUrl);

var app = builder.Build();

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};
webSocketOptions.AllowedOrigins.Add(clientBaseUrl);

app.UseWebSockets(webSocketOptions);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
