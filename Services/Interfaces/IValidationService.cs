namespace W8_Backend.Services.Interfaces
{
    public interface IValidationService
    {
        Task<bool> ValidateEmailFormatAsync(string userName);
    }
}