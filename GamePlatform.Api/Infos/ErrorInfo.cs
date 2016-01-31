using GamePlatform.Api.Infos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Infos
{
    public class ErrorInfo : IErrorInfo
    {
        public Exception Error { get; set; }

        public string Message { get; set; }
    }
}
