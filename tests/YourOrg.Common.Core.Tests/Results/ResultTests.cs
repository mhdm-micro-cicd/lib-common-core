using FluentAssertions;
using Xunit;
using YourOrg.Common.Core.Results;

namespace YourOrg.Common.Core.Tests.Results;

public class ResultTests
{
    [Fact]
    public void Success_ShouldHaveIsSuccessTrue()
    {
        var result = Result.Success();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().Be(Error.None);
    }

    [Fact]
    public void Failure_ShouldHaveIsFailureTrue()
    {
        var error = Error.NotFound("User.NotFound", "User was not found");
        var result = Result.Failure(error);
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }

    [Fact]
    public void SuccessOfT_Value_ShouldReturnValue()
    {
        var result = Result.Success(42);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(42);
    }

    [Fact]
    public void FailureOfT_AccessingValue_ShouldThrow()
    {
        var result = Result.Failure<int>(Error.NullValue);
        var act = () => result.Value;
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Match_OnSuccess_ShouldCallSuccessFunc()
    {
        var result = Result.Success("hello");
        var output = result.Match(v => v.ToUpper(), e => "error");
        output.Should().Be("HELLO");
    }

    [Fact]
    public void Match_OnFailure_ShouldCallFailureFunc()
    {
        var error = Error.NotFound("X.NotFound", "not found");
        var result = Result.Failure<string>(error);
        var output = result.Match(v => "success", e => e.Code);
        output.Should().Be("X.NotFound");
    }

    [Fact]
    public void ImplicitConversion_FromError_ShouldCreateFailureResult()
    {
        Result<int> result = Error.NullValue;
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(Error.NullValue);
    }
}
