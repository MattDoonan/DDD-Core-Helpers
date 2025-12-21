namespace DDD.Core.UnitOfWork.Exceptions;

public class InvalidUnitOfWorkRepositoryException : Exception
{
    public InvalidUnitOfWorkRepositoryException(string message) : base(message)
    {
    }
    
}