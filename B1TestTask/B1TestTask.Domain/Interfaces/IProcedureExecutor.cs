using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1TestTask.Domain.Interfaces
{
    public interface IProcedureExecutor
    {
        public Task<Dictionary<string, object>> ExecuteProcedure();

        public Task CreateProcedure();

        public Task<bool> ProcedureExists();
    }
}
