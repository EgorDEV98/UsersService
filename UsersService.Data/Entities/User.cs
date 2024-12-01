namespace UsersService.Data.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Связь
    /// </summary>
    public string? Communication { get; set; }

    /// <summary>
    /// Ключ тинькофф апи
    /// </summary>
    public required string ApiKey { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; set; }
}