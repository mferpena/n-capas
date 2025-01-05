namespace Domain;
public class TodoModel
{
    public String Id { get; set; }
    public String Name { get; set; }
    public String Description { get; set; }
    public TodoStatus Status { get; set; }
    public DeleteStatus IsDeleted { get; set; }
    public DateTime DateCreate { get; set; }

    public TodoModel(String name, String description)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Description = description;
        Status = TodoStatus.COMPLETE;
        IsDeleted = DeleteStatus.ENABLE;
        DateCreate = DateTime.Now;
    }
}
