namespace Domain;

public class TaskCompletedException : Exception
{
    public TaskCompletedException(string message) : base(message) { }
}