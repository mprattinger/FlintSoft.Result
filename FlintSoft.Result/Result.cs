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

    public T Match(Func<T> success, Func<IError, T> failure)
    {
        if (IsSuccess)
        {
            return success();
        }
        return failure(Error!);
    }

    public T Match(Func<T, T> success, Func<IError, T> failure)
    {
        if (IsSuccess)
        {
            return success(Value!);
        }
        return failure(Error!);
    }

    public T Match(Func<T> success, Func<IError, T> notFound, Func<IError, T> failure)
    {
        if (IsSuccess)
        {
            return success();
        }

        if (IsNotFound)
        {
            return notFound(Error!);
        }

        return failure(Error!);
    }

    public T Match(Func<T, T> success, Func<IError, T> notFound, Func<IError, T> failure)
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

    // public void Switch(Action<T> success, Action<IError> failure)
    // {
    //     if (IsSuccess)
    //     {
    //         success(Value!);
    //         return;
    //     }
    //     failure(Error!);
    // }

    // public void Switch(Action<T> success, Action<IError> notFound, Action<IError> failure)
    // {
    //     if (IsSuccess)
    //     {
    //         success(Value!);
    //         return;
    //     }

    //     if (IsNotFound)
    //     {
    //         notFound(Error!);
    //         return;
    //     }

    //     failure(Error!);
    // }
}
