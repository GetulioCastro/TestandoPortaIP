using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TestandoPortaIP.Services;

public class PortCheckerService
{
    private readonly ILogger<PortCheckerService> _logger;

    public PortCheckerService(ILogger<PortCheckerService> logger)
    {
        _logger = logger;
    }

    public async Task<bool> IsPortOpenAsync(string ip, int port)
    {
        try
        {
            using var client = new TcpClient();
            await client.ConnectAsync(ip, port);
            return true;
        }
        catch (SocketException)
        {
            return false;
        }
    }

    public async Task<Dictionary<string, bool>> CheckPortsAsync(List<string> ips, int port)
    {
        var result = new Dictionary<string, bool>();
        foreach (var ip in ips)
        {
            var isOpen = await IsPortOpenAsync(ip, port);
            result[ip] = isOpen;
        }
        return result;
    }
}
