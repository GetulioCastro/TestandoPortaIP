using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestandoPortaIP.Services;

namespace TestandoPortaIP.Pages;

public class DashboardModel : PageModel
{
    private readonly PortCheckerService _portCheckerService;

    public Dictionary<string, bool> PortStatuses { get; private set; } = new();

    public DashboardModel(PortCheckerService portCheckerService)
    {
        _portCheckerService = portCheckerService;
    }

    public async Task OnGet()
    {
        var ips = new List<string> { "179.179.227.92", "192.168.1.4", "179.95.180.128" }; // Adicione os IPs dos seus clientes aqui
        var port = 85; // Porta que você deseja verificar
        PortStatuses = await _portCheckerService.CheckPortsAsync(ips, port);
    }
}
