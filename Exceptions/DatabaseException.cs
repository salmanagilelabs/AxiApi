namespace AxiApi.Exceptions
{
    public class DatabaseException: Exception
    {
        public DatabaseException():base("Database Operation failed")
        {

        }

        public DatabaseException(string action) : base($"{action} Database Operation failed")
        {

        }
    }
}
