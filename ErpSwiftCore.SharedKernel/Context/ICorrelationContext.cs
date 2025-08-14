namespace ErpSwiftCore.SharedKernel.Context
{
    public interface ICorrelationContext
    {
        string CorrelationId { get; }
        void SetCorrelationId(string correlationId);
    }
}
