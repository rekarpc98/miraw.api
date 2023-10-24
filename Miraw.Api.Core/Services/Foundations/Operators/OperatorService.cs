using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Operators;

namespace Miraw.Api.Core.Services.Foundations.Operators;

public class OperatorService : IOperatorService
{
	private readonly IStorageBroker storageBroker;

	public OperatorService(IStorageBroker storageBroker)
	{
		this.storageBroker = storageBroker;
	}
	public async ValueTask<Operator> CreateOperatorAsync(Operator @operator)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Operator> RetrieveOperatorAsync(Guid operatorId)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<IQueryable<Operator>> RetrieveAllOperatorsAsync()
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Operator> ModifyOperatorAsync(Operator @operator)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Operator> RemoveOperatorAsync(Operator @operator)
	{
		throw new NotImplementedException();
	}
}