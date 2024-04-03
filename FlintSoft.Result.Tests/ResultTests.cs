using System.Text.Json.Serialization;
using FluentAssertions;

namespace FlintSoft.Result.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Result<string> res = "Test";

        res.IsSuccess.Should().BeTrue();
        res.Value.Should().Be("Test");
    }
}