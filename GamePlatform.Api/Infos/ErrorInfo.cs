using GamePlatform.Api.Infos.Interfaces;
using System;

namespace GamePlatform.Api.Infos
{
    public class ErrorInfo : IErrorInfo
    {
        public Exception Error { get; set; }

        public string Message { get; set; }
    }
}