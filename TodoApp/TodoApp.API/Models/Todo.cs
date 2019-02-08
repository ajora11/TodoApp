namespace TodoApp.API.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public int FolderId { get; set; }

        public Todo(string title, int folderId)
        {
            Title = title;
            FolderId = folderId;
            IsDone = false;
        }

        public Todo()
        {

        }
    }
}