using Microsoft.EntityFrameworkCore;
using Miraw.Api.Core.Models.ZoneOperators;

namespace Miraw.Api.Core.Brokers.Storages;

public partial class StorageBroker
{
	DbSet<ZoneOperator> ZoneOperators { get; set; }

	public async ValueTask<ZoneOperator> InsertZoneOperatorAsync(ZoneOperator zoneOperator) =>
		await InsertAsync(zoneOperator);

	public IQueryable<ZoneOperator> SelectAllZoneOperators() => SelectAll<ZoneOperator>();

	public async ValueTask<ZoneOperator?> SelectZoneOperatorByIdAsync(Guid zoneOperatorId) =>
		await SelectAsync<ZoneOperator>(zoneOperatorId);

	public async ValueTask<ZoneOperator> UpdateZoneOperatorAsync(ZoneOperator zoneOperator) =>
		await UpdateAsync(zoneOperator);

	public async ValueTask<ZoneOperator> DeleteZoneOperatorAsync(ZoneOperator zoneOperator) =>
		await DeleteAsync(zoneOperator);
}