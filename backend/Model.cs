using System.ComponentModel.DataAnnotations.Schema;

public class BaseModel
{
    public int Id { get; set; }

    public DateTime? created { get; set; }
    public DateTime? Created
    {
        get
        {
            if (created == null)
            {
                created = DateTime.UtcNow;
            }
            return created.Value;
        }
        private set { created = value; }
    }
    public DateTime? Updated { get; set; } = DateTime.UtcNow;
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
