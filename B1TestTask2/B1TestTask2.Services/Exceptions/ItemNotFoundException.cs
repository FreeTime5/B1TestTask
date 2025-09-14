namespace B1TestTask2.Services.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string itemName) : base($"{itemName} not found")
        {
        }
    }
}
