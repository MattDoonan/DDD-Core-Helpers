namespace Core.Results.Advanced.Interfaces;

public interface IInfraResult
{}

public interface IInfraConvertable : IRepoConvertable
{
    InfraResult ToInfraResult();
}

public interface IInfraConvertable<T> : IInfraConvertable, IRepoConvertable<T>
{
    InfraResult<T> ToTypedInfraResult();
}