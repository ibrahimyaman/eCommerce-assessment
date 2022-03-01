namespace eCommerce.Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(object message) : base(success: false, message)
        {
        }
        public ErrorResult() : base(success: false)
        {
        }
    }
}
