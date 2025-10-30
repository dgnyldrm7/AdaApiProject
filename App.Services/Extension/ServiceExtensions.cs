using App.Services.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace App.Services.Extension
{
    public static class ServiceExtensions
    {
        public static void AddServicesDIContainer(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ReservationService>();
        }
    }
}