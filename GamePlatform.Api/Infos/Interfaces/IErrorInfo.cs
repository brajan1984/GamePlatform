using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Infos.Interfaces
{
    public interface IErrorInfo : IInfo
    {
        Exception Error { get; set; }
    }
}
