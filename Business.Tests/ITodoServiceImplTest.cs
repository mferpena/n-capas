using Domain;

namespace Business.Tests;

public class ITodoServiceImplTest
{
    private readonly ITodoServiceImpl _todoService;

    public ITodoServiceImplTest()
    {
        _todoService = new ITodoServiceImpl();
    }

    [Fact]
    public void Add_ShouldThrowMaxTaskLimitExceededException_WhenTasksExceedLimit()
    {
        for (int i = 0; i < 10; i++)
        {
            _todoService.Add(new TodoModel($"Tarea {i}", "Descripción"));
        }

        var exception = Assert.Throws<MaxTaskLimitExceededException>(() =>
        {
            _todoService.Add(new TodoModel("Tarea 11", "Descripción"));
        });

        Assert.Equal("No se pueden crear más de 10 tareas.", exception.Message);
    }

    [Fact]
    public void Add_ShouldThrowInvalidTaskScheduleException_WhenTaskCreatedInInvalidTime()
    {
        var invalidTask = new TodoModel("Tarea nocturna", "Horario no permitido")
        {
            DateCreate = new DateTime(2025, 1, 5, 20, 0, 0) // 8 PM
        };

        var exception = Assert.Throws<InvalidTaskScheduleException>(() =>
        {
            _todoService.Add(invalidTask);
        });

        Assert.Equal("No se puede crear una tarea en el horario de 7 PM a 5 AM.", exception.Message);
    }

    [Fact]
    public void Delete_ShouldThrowTaskCompletedException_WhenTaskIsCompleted()
    {
        var completedTask = new TodoModel("Tarea completada", "Ya está completada")
        {
            Status = TodoStatus.COMPLETE
        };
        _todoService.Add(completedTask);

        var exception = Assert.Throws<TaskCompletedException>(() =>
        {
            _todoService.Delete(completedTask.Id);
        });

        Assert.Equal("No se puede eliminar una tarea que ya está completada.", exception.Message);
    }
}