namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebRole1.Models;
    using WebRole1.DAL;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<WebRole1.DAL.CloudMedContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebRole1.DAL.CloudMedContext";
        }

        protected override void Seed(WebRole1.DAL.CloudMedContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var patients = new List<Patient>
            {
                new Patient{
                    ICNo ="t0031435d",
                    Name="Jack Windsor",
                    Gender='M',
                    Citizenship="Singaporean",
                    EmailAddr="jackwindsor@jackwindsor.com",
                    Address="Buckingham Palace",
                    DoB =new DateTime(1932,2,2)
                },
                                new Patient{
                    ICNo ="S131234123D",
                    Name="Kelly Windsor",
                    Gender='F',
                    Citizenship="Singaporean",
                    EmailAddr="kelly@jacks.com",
                    Address="Wellington Palace",
                    DoB =new DateTime(1999,2,2)
                }

            };
            patients.ForEach(p => context.Patients.AddOrUpdate(x => x.Name, p));
            context.SaveChanges();

        }
    }
}
