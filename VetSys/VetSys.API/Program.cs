using Microsoft.EntityFrameworkCore;
using VetSys.Application.Dtos;
using VetSys.Infrastructure.Data;
using VetSys.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext
builder.Services.AddDbContext<VetSysApplicationContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios de repositorios
builder.Services.AddScoped<UnityOfWork>();
builder.Services.AddScoped<AnimalRepository>();
builder.Services.AddScoped<ConsultationRepository>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<MedicalHistoryRepository>();
builder.Services.AddScoped<MedicineRepository>();
builder.Services.AddScoped<MedicineTreatmentRepository>();
builder.Services.AddScoped<TreatmentRepository>();
builder.Services.AddScoped<VetRepository>();

// Agregar servicios de aplicación
builder.Services.AddScoped<AnimalService>();
builder.Services.AddScoped<ConsultationService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<MedicalHistoryService>();
builder.Services.AddScoped<MedicineService>();
builder.Services.AddScoped<MedicineTreatmentService>();
builder.Services.AddScoped<TreatmentService>();
builder.Services.AddScoped<VetService>();

// Habilitar CORS para tu frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "https://127.0.0.1:5500")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Agregar controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseCors("AllowFrontend"); // ¡Debe ir antes de MapControllers!

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

