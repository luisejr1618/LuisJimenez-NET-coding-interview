namespace SecureFlight.Core;

public class OperationResult<TResult> : OperationResult
{
    public OperationResult()
        : base()
    {
    }

    public OperationResult(TResult result)
    {
        this.Result = result;
        this.Succeeded = true;
    }

    public OperationResult(bool succeeded, TResult result)
        : this(result)
    {
        this.Succeeded = succeeded;
    }

    public OperationResult(Error error)
        : base(error)
    {
    }

    public OperationResult(Error error, TResult result)
        : base(error)
    {
        this.Result = result;
    }

    public TResult Result { get; protected set; }

    public static implicit operator TResult(OperationResult<TResult> result)
    {
        return result == null ? default : result.Result;
    }

    public static implicit operator OperationResult<TResult>(TResult result)
    {
        return new OperationResult<TResult>(result);
    }
}