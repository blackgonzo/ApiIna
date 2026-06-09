namespace inaApp.Entities.Exceptions
{
    public class DuplicateProductNameException : Exception
    {
        public DuplicateProductNameException() : base("Ya existe un producto con ese nombre.") { }

        public DuplicateProductNameException(string message) : base(message) { }
    }
}
