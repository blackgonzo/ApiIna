namespace inaApp.Entities.Exceptions
{
    public class InvalidPriceException : Exception
    {
        public InvalidPriceException() : base("El precio debe ser mayor a 0.") { }

        public InvalidPriceException(string message) : base(message) { }
    }
}
