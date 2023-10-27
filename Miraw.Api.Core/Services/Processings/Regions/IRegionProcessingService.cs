namespace Miraw.Api.Core.Services.Processings.Regions;

public interface IRegionProcessingService
{
	ValueTask<bool> VerifyRegionExistsAsync(Guid regionId);
	ValueTask ThrowIfRegionNotExistsAsync(Guid regionId);
}