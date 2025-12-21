using TODOAPP.Domain.Interfaces.IServices;

namespace TODOAPP.Services
{
    public class CorrelationIdGenerator : ICorrelationIdGenerator
    {
        private  string _correlationId = Guid.NewGuid().ToString();
        public async Task<string> Get()
            => await Task.FromResult(_correlationId.ToString());
        
        public async Task Set(string correlationId) 
            =>_correlationId = await Task.FromResult(correlationId);
    }
}