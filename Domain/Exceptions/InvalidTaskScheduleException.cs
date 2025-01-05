namespace Domain;

public class InvalidTaskScheduleException : Exception
{
    public InvalidTaskScheduleException(string message) : base(message) { }
}