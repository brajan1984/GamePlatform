using System;

namespace GamePlatform.Api.Infos.Interfaces
{
    public interface IErrorInfo : IInfo
    {
        Exception Error { get; set; }
    }
}