namespace ApplicationLayer.Commons
{
    public class AppException : Exception
    {
        public int ErrorCode { get; }

        public AppException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public AppException(int errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public AppException(int errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
