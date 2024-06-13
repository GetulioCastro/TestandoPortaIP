using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestandoPortaIP.Services;

public class PortCheckerBackgroundService : BackgroundService
{
    private readonly PortCheckerService _portCheckerService;
    private readonly ILogger<PortCheckerBackgroundService> _logger;

    public PortCheckerBackgroundService(PortCheckerService portCheckerService, ILogger<PortCheckerBackgroundService> logger)
    {
        _portCheckerService = portCheckerService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var ips = new List<string> { "179.179.227.92", "192.168.1.4", "179.95.180.128" };
            var port = 85;
            var results = await _portCheckerService.CheckPortsAsync(ips, port);

            foreach (var result in results)
            {
                if (!result.Value)
                {
                    // Enviar notificação aqui
                    _logger.LogWarning($"Porta fechada em {result.Key}");
                }
            }

            await Task.Delay(60000, stoppingToken); // Esperar 1 minuto antes da próxima verificação
        }
    }
}
