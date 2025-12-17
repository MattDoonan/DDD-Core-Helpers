using DDD.Core.Results.ValueObjects;

namespace DDD.Core.Results.Interfaces;

public interface ILayeredResult
{
    public ResultLayer CurrentLayer { get; }
    void SetCurrentLayer(ResultLayer layer);
    bool IsCurrentLayer(ResultLayer failedLayer);
}