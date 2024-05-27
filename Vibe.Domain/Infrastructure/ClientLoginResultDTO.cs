namespace Vibe.Domain.Infrastructure;

public record ClientLoginResultDTO(Guid UserId, String Token, String RefreshToken);