namespace rainbow.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SysModule",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        EnglishName = c.String(),
                        ParentId = c.String(),
                        Url = c.String(),
                        Iconic = c.String(),
                        Sort = c.Int(),
                        Remark = c.String(),
                        State = c.Boolean(),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(),
                        IsLast = c.Boolean(nullable: false),
                        Version = c.Binary(),
                        SysModule2_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SysModule", t => t.SysModule2_Id)
                .Index(t => t.SysModule2_Id);
            
            CreateTable(
                "dbo.SysSample",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Age = c.Int(),
                        Bir = c.DateTime(),
                        Photo = c.String(),
                        Note = c.String(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SysModule", "SysModule2_Id", "dbo.SysModule");
            DropIndex("dbo.SysModule", new[] { "SysModule2_Id" });
            DropTable("dbo.SysSample");
            DropTable("dbo.SysModule");
        }
    }
}
