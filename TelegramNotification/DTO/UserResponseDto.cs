namespace TelegramNotification.DTO;

public class UserResponseDto
{
    public int Id { get; set; }
    public long TelegramId { get; set; }
    public string NickName { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsEnableNotification { get; set; }
}