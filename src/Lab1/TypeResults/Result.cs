using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

public abstract record Result
{
    protected Result() { }

    public sealed record Success() : Result;

    public sealed record SuccessWithTime(double Time) : Result;

    public abstract record Failure : Result
    {
        public abstract string ErrorMessage { get; }
    }

    public sealed record InvalidSpeed(double Speed) : Failure
    {
        public override string ErrorMessage => $"Speed is invalid: {Speed}";
    }

    public sealed record NullSection(IRouteSectionService? Section) : Failure
    {
        public override string ErrorMessage => "Section can't be null.";
    }

    public sealed record ExceededMaxSpeed(double Speed) : Failure
    {
        public override string ErrorMessage => $"Speed is bigger than limit: {Speed}";
    }

    public sealed record InvalidForceApplied(double Force) : Failure
    {
        public override string ErrorMessage => $"Applied force is bigger than limit: {Force}.";
    }

    public sealed record ProcessingSectionFailure() : Failure
    {
        public override string ErrorMessage => "Section couldn't be processed.";
    }
}