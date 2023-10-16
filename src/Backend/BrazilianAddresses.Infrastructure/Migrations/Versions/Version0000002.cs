using FluentMigrator;

namespace BrazilianAddresses.Infrastructure.Migrations.Versions
{
    [Migration((long)Version.CreateTableIBGE, "Create table IBGE")]
    public class Version0000002 : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            var table = VersionBase.AddBaseColumns(Create.Table("IBGE"));

            table.WithColumn("IBGECode").AsString().NotNullable()
                .WithColumn("State").AsString().NotNullable()
                .WithColumn("City").AsString().NotNullable();
        }
    }
}
