using Microsoft.EntityFrameworkCore;
using Miraw.Api.Core.Models.Operators;
using Miraw.Api.Core.Models.ZoneOperators;

namespace Miraw.Api.Core.Brokers.Storages;

public partial class StorageBroker
{
	private static void SetZoneReferences(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ZoneOperator>().HasKey(x => new { x.ZoneId, x.OperatorId });

		modelBuilder.Entity<ZoneOperator>()
			.HasOne(x => x.Zone) // Zone has many Operators
			.WithMany(x => x.ZoneOperators) // Operator has one Zone
			.HasForeignKey(x => x.ZoneId) // ZoneId is the foreign key
			.OnDelete(DeleteBehavior.NoAction); // Do not delete Zone if Operator is deleted
		
		modelBuilder.Entity<Operator>() // Operator has many Zones
			.HasMany(x => x.ZoneOperators) // ZoneOperator has one Operator
			.WithOne(x => x.Operator) // Operator is the foreign key
			.HasForeignKey(x => x.OperatorId) // OperatorId is the foreign key
			.OnDelete(DeleteBehavior.NoAction); // Do not delete Operator if Zone is deleted
	}
}