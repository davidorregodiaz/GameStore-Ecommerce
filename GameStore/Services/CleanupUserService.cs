using GameStore.Data;

namespace GameStore.Services;

public class CleanupUserService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public CleanupUserService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromDays(5), stoppingToken);

            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var fechaLimite = DateTime.UtcNow.AddDays(-5);
                
                var usuariosInactivos = context.Users.Where(u => u.LastLogin < fechaLimite).ToList();

                if (usuariosInactivos.Any())
                {
                    context.Users.RemoveRange(usuariosInactivos);
                    await context.SaveChangesAsync();
                }
            }
        }
        
    }
}