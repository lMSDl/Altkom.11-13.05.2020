namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesConfiguration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Instructors", "FirstName", c => c.String(maxLength: 32));
            AlterColumn("dbo.Instructors", "LastName", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Instructors", "LastName", c => c.String());
            AlterColumn("dbo.Instructors", "FirstName", c => c.String());
            AlterColumn("dbo.Students", "LastName", c => c.String());
        }
    }
}
