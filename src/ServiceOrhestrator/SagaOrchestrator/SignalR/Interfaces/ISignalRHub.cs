namespace SagaOrchestrator.SignalR.Interfaces
{
    public interface ISignalRHub
    {
        Task SendStatusUpdateAsync(string message);
    }
}
