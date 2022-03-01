namespace eCommerce.Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(object message) : base(success: true, message)
        {
        }
        public SuccessResult() : base(success: true)
        {
        }
    }
}
