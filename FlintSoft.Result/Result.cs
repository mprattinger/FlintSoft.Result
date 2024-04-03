namespace FlintSoft.Result;

public class Result<T>
{
public readonly T? Value;
public readonly IError? Error;

public bool IsSuccess { get; }

public bool IsNotFound => Error is not null && Error.GetType() != typeof(Error);

public bool IsFailure => Error is not null && Error.GetType() == typeof(Error);

private Result(T value)
{
    IsSuccess = true;
    Value = value;
    Error = default;
}

private Result(IError error)
{
    IsSuccess = false;
    Value = default;
    Error = error;
}

public static implicit operator Result<T>(T value) => new Result<T>(value);

public static implicit operator Result<T>(Error error) => new Result<T>(error);

public static implicit operator Result<T>(NotFound notFound) => new Result<T>(notFound);

public Result<T> Match(Func<T, Result<T>> success, Func<IError, Result<T>> failure)
{
    if (IsSuccess)
    {
        return success(Value!);
    }
    return failure(Error!);
}

public Result<T> Match(Func<T, Result<T>> success, Func<IError, Result<T>> notFound, Func<IError, Result<T>> failure)
{
    if (IsSuccess)
    {
        return success(Value!);
    }

    if (IsNotFound)
    {
        return notFound(Error!);
    }

    return failure(Error!);
}

public void Switch(Action<T> success, Action<IError> failure)
{
    if (IsSuccess)
    {
        success(Value!);
        return;
    }
    failure(Error!);
}

public void Switch(Action<T> success, Action<IError> notFound, Action<IError> failure)
{
    if (IsSuccess)
    {
        success(Value!);
        return;
    }

    if (IsNotFound)
    {
        notFound(Error!);
        return;
    }

    failure(Error!);
}
}
