namespace OptionsPatternDemo;

public static class ServiceExtenction
{
    public static void ConfigureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<MySettings>(configuration.GetSection("MySettings"));
    }
}
