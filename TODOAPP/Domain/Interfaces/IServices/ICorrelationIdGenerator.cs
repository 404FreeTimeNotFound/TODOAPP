using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOAPP.Domain.Interfaces.IServices
{
    public interface ICorrelationIdGenerator
    {
        Task<string> Get();
        Task Set(string correlationId);
    }
}