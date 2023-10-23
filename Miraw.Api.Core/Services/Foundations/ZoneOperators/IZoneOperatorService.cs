using Miraw.Api.Core.Models.ZoneOperators;

namespace Miraw.Api.Core.Services.Foundations.ZoneOperators;

public interface IZoneOperatorService
{
	ValueTask<ZoneOperator> CreateZoneOperatorAsync(ZoneOperator zoneOperator);
	ValueTask<ZoneOperator> RetrieveZoneOperatorAsync(Guid zoneOperatorId);
	ValueTask<IQueryable<ZoneOperator>> RetrieveZoneOperatorsByZoneIdAsync(Guid zoneId);
	ValueTask<IQueryable<ZoneOperator>> RetrieveZoneOperatorsByOperatorIdAsync(Guid operatorId);
	ValueTask<IQueryable<ZoneOperator>> RetrieveAllZoneOperatorsAsync();
	ValueTask<ZoneOperator> ModifyZoneOperatorAsync(ZoneOperator zoneOperator);
	ValueTask<ZoneOperator> RemoveZoneOperatorAsync(ZoneOperator zoneOperator);
}