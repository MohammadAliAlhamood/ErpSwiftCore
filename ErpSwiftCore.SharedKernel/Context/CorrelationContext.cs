using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.SharedKernel.Context
{
    public class CorrelationContext : ICorrelationContext
    {
        private static readonly AsyncLocal<string?> _correlationId = new();

        public string CorrelationId
        {
            get
            {
                if (string.IsNullOrEmpty(_correlationId.Value))
                {
                    // إذا لم يُعيّن مسبقاً، ننشئ GUID جديد
                    _correlationId.Value = System.Guid.NewGuid().ToString();
                }
                return _correlationId.Value;
            }
        }

        public void SetCorrelationId(string correlationId)
        {
            _correlationId.Value = correlationId;
        }
    }
}