namespace FlintSoft.Result;

public class NotFound : IError
{
    public string Code { get; set; }
    public string Description { get; set; }

    public NotFound() : this(string.Empty, string.Empty)
    {

    }

    public NotFound(string description) : this(string.Empty, description)
    {

    }

    public NotFound(string code, string description)
    {
        Code = code;
        Description = description;
    }
}

public static class NotFoundExtensions {
    public static NotFound ToNotFound(this IError fromNotFound) {
        if (fromNotFound is NotFound) {
            return new (fromNotFound.Code == "" ? string.Empty : fromNotFound.Code, fromNotFound.Description);
        } else {
            throw new ArgumentException("fromNotFound is not a valid NotFound Type!");
        }
    }
}