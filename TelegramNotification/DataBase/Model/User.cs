namespace TelegramNotification.DataBase.Model
{
    public class User
    {
        public int Id { get; set; } 
        public long TelegramId { get; set; }
        public string NickName { get; set; } = null!;
        public DateTime CreationDate { get; set; }
    }
}