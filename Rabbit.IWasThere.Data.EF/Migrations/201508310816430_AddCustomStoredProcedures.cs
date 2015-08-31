namespace Rabbit.IWasThere.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddCustomStoredProcedures : DbMigration
    {
        public override void Up()
        {
            Sql(UpScripts);
        }

        public override void Down()
        {
            Sql(DownScripts);
        }

        private string DownScripts
        {
            get
            {
                return @"
/****** Object:  StoredProcedure [dbo].[CountMessages]    Script Date: 08/31/2015 15:30:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountMessages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CountMessages]
GO
";
            }
        }

        private string UpScripts
        {
            get
            {
                return @"
/****** Object:  StoredProcedure [dbo].[CountMessages]    Script Date: 08/31/2015 15:29:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountMessages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CountMessages]
GO

CREATE PROC [dbo].[CountMessages]
AS
BEGIN
	SELECT CategoryId, COUNT(m.Id) MessageCount FROM Message m
	GROUP BY CategoryId
	UNION
	SELECT '7573A0E7-B94B-4752-AFE5-A426DD9B454A' AS CategoryId, COUNT(m.Id) FROM Message m
END
GO
";
            }
        }
    }
}
