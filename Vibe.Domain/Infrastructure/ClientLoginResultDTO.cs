namespace Vibe.Domain.Infrastructure;

public record ClientLoginResultDTO(Guid ClientId, String Token, String RefreshToken);