using Microsoft.EntityFrameworkCore;
using VetSys.Application.Dtos;
using VetSys.Infrastructure.Data;
using VetSys.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VetSysApplicationContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UnityOfWork>();
builder.Services.AddScoped<AnimalRepository>();
builder.Services.AddScoped<ConsultationRepository>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<MedicalHistoryRepository>();
builder.Services.AddScoped<MedicineRepository>();
builder.Services.AddScoped<MedicineTreatmentRepository>();
builder.Services.AddScoped<TreatmentRepository>();
builder.Services.AddScoped<VetRepository>();

builder.Services.AddScoped<AnimalService>();
builder.Services.AddScoped<ConsultationService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<MedicalHistoryService>();
builder.Services.AddScoped<MedicineService>();
builder.Services.AddScoped<MedicineTreatmentService>();
builder.Services.AddScoped<TreatmentService>();
builder.Services.AddScoped<VetService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAllOrigins");
app.Run();
