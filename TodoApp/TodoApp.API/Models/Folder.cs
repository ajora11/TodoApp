namespace TodoApp.API.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }

        public Folder(string title, int userId)
        {
            Title = title;
            UserId = userId;
        }

        public Folder()
        {
           
        }
    }
}