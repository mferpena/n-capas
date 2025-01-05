namespace Domain;

public class MaxTaskLimitExceededException : Exception
{
    public MaxTaskLimitExceededException(string message) : base(message) { }
}