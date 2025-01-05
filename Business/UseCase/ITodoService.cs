using Domain;

namespace Business;
public interface ITodoService
{
    List<TodoModel> ListAll();
    TodoModel? GetById(string id);
    List<TodoModel> SearchByName(string name);
    void Add(TodoModel todo);
    void Update(string id, TodoModel updatedTodo);
    void Delete(string id);
}
