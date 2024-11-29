namespace ReservationSystem.Areas.User.Services;

public class ReservationStatusUpdater : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private Timer? _timer;

    public ReservationStatusUpdater(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(UpdateStatuses, null, TimeSpan.Zero, TimeSpan.FromHours(24));
        return Task.CompletedTask;
    }

    private async void UpdateStatuses(object? state)
    {
        using var scope = _scopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<ReservationService>();
        await service.UpdateCompletedReservations();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}

