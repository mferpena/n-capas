using Domain;

namespace Business;
public class ITodoServiceImpl : ITodoService
{
    private readonly List<TodoModel> _todoList = new List<TodoModel>();
    private const int MaxTasks = 10;

    public void Add(TodoModel todo)
    {
        if (_todoList.Count >= MaxTasks)
        {
            throw new MaxTaskLimitExceededException("No se pueden crear más de 10 tareas.");
        }

        var horaInicioNoPermitida = 19;
        var horaFinNoPermitida = 5;
        if (todo.DateCreate.Hour >= horaInicioNoPermitida || todo.DateCreate.Hour < horaFinNoPermitida)
        {
            throw new InvalidTaskScheduleException("No se puede crear una tarea en el horario de 7 PM a 5 AM.");
        }

        _todoList.Add(todo);
    }

    public void Delete(string id)
    {
        var todo = GetById(id);
        if (todo == null)
        {
            throw new ArgumentException($"No se encontró la tarea con ID: {id}");
        }

        if (todo.Status == TodoStatus.COMPLETE)
        {
            throw new TaskCompletedException("No se puede eliminar una tarea que ya está completada.");
        }

        todo.IsDeleted = DeleteStatus.DISABLED;
    }

    public TodoModel? GetById(string id)
    {
        return _todoList.FirstOrDefault(t => t.Id == id && t.IsDeleted == DeleteStatus.ENABLE);
    }

    public List<TodoModel> ListAll()
    {
        return _todoList.Where(t => t.IsDeleted == DeleteStatus.ENABLE).ToList();
    }

    public List<TodoModel> SearchByName(string name)
    {
        return _todoList
            .Where(t => t.Name.Contains(name, StringComparison.OrdinalIgnoreCase) && t.IsDeleted == DeleteStatus.ENABLE)
            .ToList();
    }

    public void Update(string id, TodoModel updatedTodo)
    {
        var todo = GetById(id);
        if (todo == null)
        {
            throw new ArgumentException($"No se encontró la tarea con ID: {id}");
        }

        todo.Name = updatedTodo.Name;
        todo.Description = updatedTodo.Description;
        todo.Status = updatedTodo.Status;
    }
}
