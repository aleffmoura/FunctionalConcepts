namespace FunctionalConcepts.Tests.Results;

using FluentAssertions;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;

[TestFixture]
public class ResultFailWhen
{
    [Test]
    public void Result_FailWhen_ShouldBeOk()
    {
        // arrange
        var userName = "nomedeusuario";
        Result<string> result = userName;
        var error = "nome de usuario não pode ser menor que 5";

        // action
        var response = result.FailWhen(x => x.Length <= 5, (InvalidObjectError)error);

        // assert
        response.IsSuccess.Should().BeTrue();
        response.IsFail.Should().BeFalse();
    }

    [Test]
    public void Result_FailWhen_FailInValidation_ShouldReturnFail_WithInvalidObjectError()
    {
        // arrange
        var userName = "nome";
        Result<string> result = userName;
        var error = "nome de usuario não pode ser menor que 5";

        // action
        var response = result.FailWhen(x => x.Length <= 5, (InvalidObjectError)error);

        // assert
        response.IsSuccess.Should().BeFalse();
        response.IsFail.Should().BeTrue();
        response.Else(fail =>
        {
            fail.Message.Should().Be(error);
        });
    }
}
