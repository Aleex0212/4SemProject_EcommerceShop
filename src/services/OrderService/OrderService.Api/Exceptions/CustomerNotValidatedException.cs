namespace OrderService.Api.Exceptions
{
  public class CustomerNotValidatedException : Exception
  {
    public CustomerNotValidatedException()
    {
    }
    public CustomerNotValidatedException(string message) : base(message)
    {
    }
    public CustomerNotValidatedException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}
