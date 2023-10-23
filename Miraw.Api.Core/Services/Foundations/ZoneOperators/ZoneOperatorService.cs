using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.ZoneOperators;

namespace Miraw.Api.Core.Services.Foundations.ZoneOperators;

public class ZoneOperatorService : IZoneOperatorService
{
	readonly IStorageBroker storageBroker;

	public ZoneOperatorService(IStorageBroker storageBroker)
	{
		this.storageBroker = storageBroker;
	}
	public async ValueTask<ZoneOperator> CreateZoneOperatorAsync(ZoneOperator zoneOperator)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<ZoneOperator> RetrieveZoneOperatorAsync(Guid zoneOperatorId)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<IQueryable<ZoneOperator>> RetrieveZoneOperatorsByZoneIdAsync(Guid zoneId)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<IQueryable<ZoneOperator>> RetrieveZoneOperatorsByOperatorIdAsync(Guid operatorId)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<IQueryable<ZoneOperator>> RetrieveAllZoneOperatorsAsync()
	{
		throw new NotImplementedException();
	}

	public async ValueTask<ZoneOperator> ModifyZoneOperatorAsync(ZoneOperator zoneOperator)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<ZoneOperator> RemoveZoneOperatorAsync(ZoneOperator zoneOperator)
	{
		throw new NotImplementedException();
	}
}