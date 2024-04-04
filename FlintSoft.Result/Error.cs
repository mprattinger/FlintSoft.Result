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
