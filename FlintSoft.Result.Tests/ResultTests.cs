using System.Text.Json.Serialization;
using FluentAssertions;

namespace FlintSoft.Result.Tests;

public class ResultTests
{
    [Fact]
    public void Sucess()
    {
        Result<string> res = "Test";

        res.IsSuccess.Should().BeTrue();
        res.IsNotFound.Should().BeFalse();
        res.IsFailure.Should().BeFalse();

        res.Value.Should().Be("Test");
    }

    [Fact]
    public void NotFound()
    {
        Result<string> res = new NotFound();

        res.IsSuccess.Should().BeFalse();
        res.IsNotFound.Should().BeTrue();
        res.IsFailure.Should().BeFalse();
    }

    [Fact]
    public void Error()
    {
        Result<string> res = new Error("MyError", "There was an error");

        res.IsSuccess.Should().BeFalse();
        res.IsNotFound.Should().BeFalse();
        res.IsFailure.Should().BeTrue();

        res.Error.Should().NotBeNull();
        res.Error!.Description.Should().Be("There was an error");
    }

    [Fact]
    public void ErrorwithException()
    {
        Result<string> res = new Error(new ArgumentNullException("Test", new Exception("inner")));

        res.IsSuccess.Should().BeFalse();
        res.IsNotFound.Should().BeFalse();
        res.IsFailure.Should().BeTrue();

        res.Error.Should().NotBeNull();
        res.Error!.Description.Should().Be("Test");
    }
}