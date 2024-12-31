namespace CiFarmSDK.Utils
{
    public class Builder<T>
        where T : new()
    {
        private readonly T _instance;

        // Constructor initializes a new instance of T
        public Builder()
        {
            _instance = new T();
        }

        // Build method that returns the created object
        public T Build()
        {
            return _instance;
        }
    }
}
