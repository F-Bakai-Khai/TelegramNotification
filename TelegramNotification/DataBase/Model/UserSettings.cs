namespace TelegramNotification.DataBase.Model;

public class UserSettings
{
    public int UserId { get; set; }
    public bool IsEnableNotification { get; set; } = true;

    public User User { get; set; }
}