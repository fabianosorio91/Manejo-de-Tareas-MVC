using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC.Migrations
{
    /// <inheritdoc />
    public partial class AdminRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
if not exists (select Id from AspNetRoles where Id ='79e41eb3-f313-4794-9f2f-4d2e8210ddf5')
begin 
insert AspNetRoles(Id, [Name], [NormalizedName])
values ('79e41eb3-f313-4794-9f2f-4d2e8210ddf5', 'admin', 'ADMIN')
end"
);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE AspNetRoles Where Id '79e41eb3 - f313 - 4794 - 9f2f - 4d2e8210ddf5'");
        }
    }
}
