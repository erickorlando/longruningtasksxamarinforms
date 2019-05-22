namespace MyGPSLogic.DataTransferObjects
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
    }
}