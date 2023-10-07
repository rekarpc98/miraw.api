using Miraw.Api.Core.Models.ZoneOperators;

namespace Miraw.Api.Core.Brokers.Storages;

public partial interface IStorageBroker
{
    ValueTask<ZoneOperator> InsertZoneOperatorAsync(ZoneOperator zoneOperator);
    IQueryable<ZoneOperator> SelectAllZoneOperators();
    ValueTask<ZoneOperator?> SelectZoneOperatorByIdAsync(Guid zoneOperatorId);
    ValueTask<ZoneOperator> UpdateZoneOperatorAsync(ZoneOperator zoneOperator);
    ValueTask<ZoneOperator> DeleteZoneOperatorAsync(ZoneOperator zoneOperator);
}