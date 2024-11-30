namespace OrderService.Api.Exceptions
{
  public class ConfirmOrderException :Exception
  {
    public ConfirmOrderException()
    {
    }
    public ConfirmOrderException(string message) : base(message)
    {
    }
    public ConfirmOrderException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}
