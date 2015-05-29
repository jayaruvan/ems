using System;
using System.Collections.Generic;
using System.Data.Entity;

using EMS.Common;
using EMS.Core.Entities;
using EMS.Core;
using EMS.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace EMS.UnitTest
{
    
    [TestClass]
    public class ProvinceTest
    {

      [TestMethod]
        public void AddProvince()
        {
            Database.SetInitializer<EmsDbContext>(new  DropCreateDatabaseAlways<EmsDbContext>());
            using (var context = new EmsDbContext())
            {
                //context.Database.Create();
                Province province = new Province();
                province = EntityFactory.CreateProvince("Province A");

                context.Entry(province).State = System.Data.EntityState.Added;
                context.SaveChanges();
                              
            }
        }
        
    [TestMethod]
    public void AddArea()
    {
        Database.SetInitializer<EmsDbContext>(new DropCreateDatabaseAlways<EmsDbContext>());
        using (var context = new EmsDbContext())
        {
            //context.Database.Create();
            Province province = new Province();
            province = EntityFactory.CreateProvince("Province A");

            context.Entry(province).State = System.Data.EntityState.Added;
            context.SaveChanges();


            Area area = new Area();
            area = EntityFactory.CreateArea("Area A", 1);

            context.Entry(area).State = System.Data.EntityState.Added;
            context.SaveChanges();

            Area area1 = new Area();
            area1 = EntityFactory.CreateArea("Area B", 1);

            context.Entry(area1).State = System.Data.EntityState.Added;
            context.SaveChanges();

        }
    }

       

    [TestMethod]
    public void AddSubStation()
    {
        Database.SetInitializer<EmsDbContext>(new DropCreateDatabaseAlways<EmsDbContext>());
        using (var context = new EmsDbContext())
        {
            //context.Database.Create();
            Province province = new Province();
            province = EntityFactory.CreateProvince("Province A");

            context.Entry(province).State = System.Data.EntityState.Added;
            context.SaveChanges();


            Area area = new Area();
            area = EntityFactory.CreateArea("Area A", 1);

            context.Entry(area).State = System.Data.EntityState.Added;
            context.SaveChanges();

            Area area1 = new Area();
            area1 = EntityFactory.CreateArea("Area B", 1);

            context.Entry(area1).State = System.Data.EntityState.Added;
            context.SaveChanges();

            SubStation sub1 = new SubStation();
            sub1 = EntityFactory.CreateSubStation("Sub Statoin A", 1,1);
            context.Entry(sub1).State = System.Data.EntityState.Added;
            context.SaveChanges();

            
            SubStation sub2 = new SubStation();
            sub2 = EntityFactory.CreateSubStation("Sub Statoin B", 1, 1);
            context.Entry(sub2).State = System.Data.EntityState.Added;
            context.SaveChanges();
         
        }
    }
    }

}

