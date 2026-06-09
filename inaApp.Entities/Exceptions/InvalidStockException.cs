namespace inaApp.Entities.Exceptions
{
    public class InvalidStockException : Exception
    {
        public InvalidStockException() : base("El stock debe ser mayor a 0.") { }

        public InvalidStockException(string message) : base(message) { }
    }
}
