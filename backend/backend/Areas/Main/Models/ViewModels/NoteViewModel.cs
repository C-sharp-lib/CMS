namespace backend.Areas.Main.Models.ViewModels;

public class AddNoteViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}

public class UpdateNoteViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}

public class AddUserNoteViewModel
{
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}
public class UpdateUserNoteViewModel
{
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}