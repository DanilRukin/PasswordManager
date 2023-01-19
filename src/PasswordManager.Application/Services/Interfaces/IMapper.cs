using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.Services.Interfaces
{
    public interface IMapper<TSource, TDest>
    {
        TDest Map(TSource source);
    }
}
