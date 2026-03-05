namespace CoopApplication.api.Extension
{
    public static class ConfigureExtension
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "COOP APPLICATION API v1"));
        }

        public static void ConfigureCORS(this IApplicationBuilder app)
        {
            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }
    }
}
