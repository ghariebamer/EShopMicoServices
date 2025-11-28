using Marten;


var builder = WebApplication.CreateBuilder(args);


// add DI For sevices here
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>()); // or can use that for mediatR

// add marten 
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database"));
    options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
}).UseLightweightSessions(); // Enables scoped sessions

var app = builder.Build();



// add middleware here
app.MapCarter();
app.Run();
