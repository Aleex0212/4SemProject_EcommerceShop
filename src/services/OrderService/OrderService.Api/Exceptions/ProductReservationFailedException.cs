namespace OrderService.Api.Exceptions
{
  public class ProductReservationFailedException : Exception
  {
    public ProductReservationFailedException()
    {

    }
    public ProductReservationFailedException(string message) : base(message)
    {

    }
    public ProductReservationFailedException(string message, Exception innerException) : base(message, innerException)
    {

    }
  }
}
