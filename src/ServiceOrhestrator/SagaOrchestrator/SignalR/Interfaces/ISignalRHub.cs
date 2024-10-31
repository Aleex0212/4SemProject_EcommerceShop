namespace SagaOrchestrator.SignalR.Interfaces
{
  /// <summary>
  /// Interface for sending status updates via SignalR.
  /// </summary>
  public interface ISignalRHub
  {
    /// <summary>
    /// Sends a status update message.
    /// </summary>
    /// <param name="message">The status message to be sent.</param>
    Task SendStatusUpdateAsync(string message);
  }
}