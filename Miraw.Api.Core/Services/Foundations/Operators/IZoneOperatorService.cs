using Miraw.Api.Core.Models.Operators;

namespace Miraw.Api.Core.Services.Foundations.Operators;

public interface IOperatorService
{
	ValueTask<Operator> CreateOperatorAsync(Operator @operator);
	ValueTask<Operator> RetrieveOperatorAsync(Guid operatorId);
	ValueTask<IQueryable<Operator>> RetrieveAllOperatorsAsync();
	ValueTask<Operator> ModifyOperatorAsync(Operator @operator);
	ValueTask<Operator> RemoveOperatorAsync(Operator @operator);
}