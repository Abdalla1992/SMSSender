using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smsSender.Data.Infrastructure.Dto
{
    public class ResultBaseDto<T>
    {
        public ResultBaseDto()
        { }

        public ResultBaseDto(T result, string defaultErrorMessage)
        {
            Result = result;
            Message = defaultErrorMessage;
        }


        public T Result { get; set; }
        public string Message { get; set; }

        public void SetResult(T result, string message)
        {
            Result = result;
            Message = message;

        }

        public void ConcatMessage(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                Message = Message + " , " + message;
        }

    }
}
