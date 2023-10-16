using FluentMigrator.Builders.Create.Table;

namespace BrazilianAddresses.Infrastructure.Migrations
{
    public static class VersionBase
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax AddBaseColumns(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table)
        {
            return table.WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("CreationDate").AsDateTime().NotNullable()
                .WithColumn("CreationUserId").AsInt64().NotNullable()
                .WithColumn("UpdateDate").AsDateTime().NotNullable()
                .WithColumn("UpdateUserId").AsInt64().NotNullable()
                .WithColumn("DeletionDate").AsDateTime().Nullable();
        }
    }
}
