using SignalR_Test;

var builder = WebApplication.CreateBuilder(args);

//CORS�]�w
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//���U�����A��
builder.Services.AddSignalR();
builder.Services.AddSingleton<TimerManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

//�]�wSignalR Hub���Ѹ��|
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MyHub>("/myHub");
});

app.UseAuthorization();
app.MapControllers();
app.Run();
