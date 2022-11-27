using System.ComponentModel.DataAnnotations.Schema;

public class BaseModel
{
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? Created { get; set; } = DateTime.UtcNow;
    public DateTime? Deleted { get; set; }
}

public class Record : BaseModel
{

}
public class Event : BaseModel
{

}
public class EventData : BaseModel
{

}
