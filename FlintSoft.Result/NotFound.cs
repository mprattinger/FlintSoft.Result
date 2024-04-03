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
