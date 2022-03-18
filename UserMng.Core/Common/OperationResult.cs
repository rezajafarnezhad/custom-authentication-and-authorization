namespace UserMng.Core.Common
{
    public class OperationResult
    {
        public string Message { get; set; }
        public string Data { get; set; }
        public bool isSucceeded { get; set; }

        public OperationResult()
        {
            isSucceeded = false;
        }

        public OperationResult Succeeded(string message = "عملیات با موفقیت انجام شد")
        {
            isSucceeded = true;
            Message = message;
            return this;
        }

        public OperationResult Succeeded(string message = "عملیات با موفقیت انجام شد",string data="")
        {
            isSucceeded = true;
            Message = message;
            Data = data;
            return this;
        }

        public OperationResult Failed(string message = "عملیات با شکست مواجه شد")
        {
            isSucceeded = false;
            Message = message;
            return this;
        }

        public OperationResult Failed(object wrongUserinformation)
        {
            throw new NotImplementedException();
        }
    }
}
