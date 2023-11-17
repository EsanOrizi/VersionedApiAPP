using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Swagger config for versioning
builder.Services.AddSwaggerGen(opts =>
{
    var title = "Our versioned API";
    var description = "This is our description";
    var terms = new Uri("https://localhost:7117");
    var license = new OpenApiLicense()
    { Name = "our license" };
    var contact = new OpenApiContact()
    { Name = "Esan" };

    opts.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1 Deprecated" ,
        Title = title,
        Description = description,
        TermsOfService = terms,
        License = license,
        Contact = contact
    });

    opts.SwaggerDoc("v2", new OpenApiInfo()
    {
        Version = "v2",
        Title = title,
        Description = description,
        TermsOfService = terms,
        License = license,
        Contact = contact
    });


});
builder.Services.AddApiVersioning(opts =>
{
    opts.AssumeDefaultVersionWhenUnspecified = true;
    opts.DefaultApiVersion = new(1, 0);
    opts.ReportApiVersions = true;

});

builder.Services.AddVersionedApiExplorer(opts =>
{
    opts.GroupNameFormat = "'v'VVV";
    opts.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
        opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
