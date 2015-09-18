namespace Rabbit.IWasThere.Services.DirectImpl
{
    public class DirectDataServiceFactory : IDataServiceFactory
    {
        public IDataService Create()
        {
            return new DirectDataService();
        }
    }
}