namespace Utilities;

public static class OperationResultExtensions
{
    public static TOperationResult AppendErrors<TOperationResult>(this TOperationResult principal, OperationResult other)
        where TOperationResult : OperationResult
    {
        if (principal is null) throw new ArgumentNullException(nameof(principal));

        foreach (var error in (other?.Errors).OrEmptyIfNull().IgnoreNullValues()) principal.AddError(error);
        return principal;
    }

    public static OperationResult<TData> WithData<TData>(this OperationResult<TData> principal, TData data)
    {
        if (principal is null) throw new ArgumentNullException(nameof(principal));
        
        principal.Data = data;
        return principal;
    }

    public static void AppendException(this OperationResult principal, Exception ex)
    {
        if (principal is null) throw new ArgumentNullException(nameof(principal));
        if (ex is null) return;

        var error = new Error { IsNotExpected = true, Message = ex.Message };
        principal.AddError(error);
    }
}