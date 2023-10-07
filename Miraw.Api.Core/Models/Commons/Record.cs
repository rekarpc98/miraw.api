namespace Miraw.Api.Core.Models.Commons;

public class Record
{
	public DateTimeOffset CreatedDate { get; set; }
	public string CreatedBy { get; set; } = null!;
}