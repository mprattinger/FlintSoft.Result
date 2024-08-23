using System.Runtime.CompilerServices;

namespace FlintSoft.Result;

public class Error : IError
{
    public string Code { get; set; }
    public string Description { get; set; }

    public IError? InnerError { get; set; }

    public Exception? Exception { get; set; }

    public Error(string code, string description, IError? innerError = null)
    {
        Code = code;
        Description = description;
        InnerError = innerError;
    }

    public Error(string code, string description) : this(code, description, null) { }

    public Error(string description) : this(string.Empty, description)
    {

    }

    public Error(Exception ex, [CallerMemberName] string memberName = "", string enrichMsg = "") : this(memberName, $"{enrichMsg} {ex.Message}".Trim())
    {
        Exception = ex;
    }
}

public static class ErrorExtensions
{
    public static Error ToError(this IError fromError)
    {
        if (fromError is Error)
        {
            return new(fromError.Code == "" ? string.Empty : fromError.Code, fromError.Description);
        }
        else
        {
            throw new ArgumentException("fromError is not a valid Error Type!");
        }
    }

    public static Error MergeError(this IError fromError, string description, string key = "")
    {
        var ret = fromError;

        if (!string.IsNullOrEmpty(key)) ret.Code = key;

        ret.Description = $"{description}, {ret.Description}";

        return (ret as Error)!;
    }
}