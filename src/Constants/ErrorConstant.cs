using FunctionalConcepts.Errors;

namespace FunctionalConcepts.Constants;
public class ErrorConstant
{
    public static string RESULT_IS_BOTTOM { get => "result was not initialized correctly, see documentation. Result is bottom"; }
    public static BaseError BOTTOM { get => (500, RESULT_IS_BOTTOM); }
}
