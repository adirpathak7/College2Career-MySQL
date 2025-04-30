namespace College2Career.HelperServices
{
    public class ServiceResponse<T>
    {
        public T data { get; set; }
        public string message { get; set; }
        public bool status { get; set; }
    }
}
