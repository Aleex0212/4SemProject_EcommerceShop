using Microsoft.AspNetCore.SignalR;
using SagaOrchestrator.SignalR.Interfaces;

public class StatusUpdateHub : Hub<ISignalRHub>
{
    public async Task SendStatusUpdateAsync(string message)
    {
        await Clients.All.SendStatusUpdateAsync(message);
    }
}