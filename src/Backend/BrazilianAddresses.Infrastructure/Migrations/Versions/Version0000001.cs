using FluentMigrator;

namespace BrazilianAddresses.Infrastructure.Migrations.Versions
{
    [Migration((long)Version.CreateTableUser, "Create table User")]
    public class Version0000001 : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            var table = VersionBase.AddBaseColumns(Create.Table("User"));

            table.WithColumn("Email").AsString().NotNullable()
                .WithColumn("Password").AsString(2000).NotNullable()
                .WithColumn("Salt").AsString().NotNullable()
                .WithColumn("Role").AsInt32().NotNullable();
        }
    }
}
