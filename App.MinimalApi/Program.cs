using App.Core.DTO;
using App.Core.Entities;
using App.MinimalApi.Middlewares;
using App.Services.Extension;
using App.Services.Service;
using App.Services.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ------------------- JSON Configuration -------------------
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Add(AppJsonSerializerContext.Default);
});

// ------------------- Swagger & Documentation -------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------- FluentValidation -------------------
builder.Services.AddFluentValidationAutoValidation(); 
// required for ASP.NET Core integration
builder.Services.AddValidatorsFromAssemblyContaining<ReservationRequestValidator>();

// ------------------- Custom Application Services -------------------
builder.AddServicesDIContainer();

// ------------------- Build Application -------------------
var app = builder.Build();

// ------------------- Swagger Setup -------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ------------------- Middleware Pipeline -------------------
app.UseValidationExceptionHandling(); 
// Custom: FluentValidation Error Formatter
app.UseGlobalExceptionHandling();     
// Custom: Global Exception Catcher

// ------------------- API Endpoint -------------------
app.MapPost("/api/reservation", async (
    ReservationRequest request,
    IValidator<ReservationRequest> validator,
    ReservationService service) =>
{
    var validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));

        var errorResponse = new ErrorResponseDto
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation Error",
            Detail = "One or more validation errors occurred.",
            ErrorMessage = errors
        };

        return Results.Json(
            errorResponse,
            AppJsonSerializerContext.Default.ErrorResponseDto,
            statusCode: StatusCodes.Status400BadRequest
        );
    }

    var result = service.Calculate(request);

    return Results.Extensions.FromResult(result);
})
.WithName("CreateReservation")
.WithDescription("Creates a new train reservation based on seat availability and 70% occupancy rule.")
.Produces<ReservationResponse>(StatusCodes.Status200OK)
.Produces<ErrorResponseDto>(StatusCodes.Status400BadRequest)
.Produces<ErrorResponseDto>(StatusCodes.Status422UnprocessableEntity)
.Produces<ErrorResponseDto>(StatusCodes.Status500InternalServerError);

app.MapGet("/", () => "Welcome to the Train Reservation API!");

app.MapGet("/api/reservation", () => "This API only allows POST request.");

// ------------------- Run Application -------------------
app.Run();

// ------------------- JSON Source Generator -------------------
[JsonSerializable(typeof(ReservationRequest))]
[JsonSerializable(typeof(ReservationResponse))]
[JsonSerializable(typeof(ErrorResponseDto))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
