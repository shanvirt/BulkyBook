using Microsoft.EntityFrameworkCore.Migrations;

namespace BulkyBook.DataAccess.Migrations
{
    public partial class addStoredProcForCoverType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC ups_GetCoverTypes
                                                AS
                                                BEGIN
                                                SELECT * FROM dbo.CoverTypes
                                                END");
            migrationBuilder.Sql(@"CREATE PROC ups_GetCoverType
                                                @Id int
                                                AS
                                                BEGIN
                                                SELECT * FROM dbo.CoverTypes WHERE (Id=@Id)
                                                END");
            migrationBuilder.Sql(@"CREATE PROC ups_UpdateCoverType
                                                @Id int,
                                                @Name varchar(100)
                                                AS
                                                BEGIN
                                                UPDATE dbo.CoverType
                                                SET Name=@Name
                                                WHERE Id=@Id
                                                END");
            migrationBuilder.Sql(@"CREATE PROC ups_DeleteCoverType
                                                    @Id int
                                                    AS
                                                    BEGIN
                                                    DELETE FROM dbo.CoverType WHERE (Id=@Id)
                                                    END");
            migrationBuilder.Sql(@"CREATE PROC ups_CreateCoverType
                                                    @Name varchar(100)
                                                    AS
                                                    BEGIN
                                                    INSERT INTO dbo.CoverType(Name)
                                                    VALUES (@Name)
                                                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetCoverTypes");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetCoverType");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_UpdateCoverTypes");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_DeleteCoverTypes");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_CreateCoverTypes");



        }
    }
}
