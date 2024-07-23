using System.Runtime.CompilerServices;

namespace FlintSoft.Result;

public class Error : IError
{
    public string Code { get; set; }
    public string Description { get; set; }

    public Error(string code, string description)
    {
        Code = code;
        Description = description;
    }

    public Error(string description) : this(string.Empty, description)
    {

    }

    public Error(Exception ex, [CallerMemberName] string memberName = "") : this(memberName, ex.Message)
    {
        
    }
}

public static class ErrorExtensions {
    public static Error FromIError(this IError fromError) {
        if (fromError is Error) {
            return new (fromError.Code == "" ? string.Empty : fromError.Code, fromError.Description);
        } else {
            throw new ArgumentException("fromError is not a valid Error Type!");
        }
    }
}