﻿using Asa.ApartmentSystem.ApplicationService;
using Asa.Draft.EF;
using System;
using System.Reflection;
using System.Linq;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Asa.Draft.Domain;
using Microsoft.EntityFrameworkCore;
using ASa.ApartmentManagement.Core.ChargeCalculation.Domain.CalculationFormula;
using Asa.ApartmentSystem.Infra.DataGateways;
using System.Data.SqlClient;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;

namespace Asa.Draft
{
    class Program
    {


        static AutoResetEvent autoResetEvent;
        static async Task Main(string[] args)
        {
            //autoResetEvent = new AutoResetEvent(false);
            //Console.WriteLine("Hello World!");

            ////Thread myWorker1 = new Thread(() => DoMyFirstJob());
            ////myWorker1Start();

            ////Thread myWorker2 = new Thread(() => DoMySecondJob());
            ////myWorker2.Start();

            //Console.WriteLine($"Main thread id { Thread.CurrentThread.ManagedThreadId} ");

            //var task1 = Task.Run(DoMyFirstJob);
            //var task2 = Task.Run(DoMySecondJob);
            //await Task.WhenAll(task1, task2);

            //var connectionString = ConfigurationManager.ConnectionStrings["ApartmentManagementCNX"].ConnectionString;
            var connectionString = "Data Source=193.151.128.227,1433;Initial Catalog=AsaApartmentManagementDB;User ID=teamuser;Password=whiterose;";
            //var connectionString = @"Data Source = PC08\Asa\SQLEXPRESS; Initial Catalog = Building; Integrated Security = True" ;
            //var baseInfoService = new PersonInfoApplicationService(connectionString);
            //var personDTO = await baseInfoService.GetPersonByIdAsync(21);

            /*var person = new PersonDTO();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpGetPersonById]";
                    cmd.Parameters.AddWithValue("@personId", 21);
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    using (var dataReader = await cmd.ExecuteReaderAsync())
                    {
                        await dataReader.ReadAsync();
                        person.Id = dataReader.Extract<int>("PersonId");
                        person.FullName = dataReader.Extract<string>("FullName");
                        person.PhoneNumber = dataReader.Extract<string>("PhoneNumber");
                    }
                }
            }

            var name = person.FullName;*/

            //var personTableGateway = new PersonTableGateway(connectionString);
            //var person = await personTableGateway.GetPersonByIdAsync(21);
            //Console.WriteLine(baseInfoService.GetPersonByIdAsync(21));
            //var units = await baseInfoService.GetAllOwnerTenantByUnitId(1);

            var buildingInfoService = new BuildingInfoApplicationService(connectionString);
            //var units = await buildingInfoService.GetUnitsByPage(1, 2);
            //var unit = await buildingInfoService.GetUnit(4);
            //var buildingDTO = await buildingInfoService.GetFirstBuilding();
            //int id = buildingDTO.Id;

            int id = await buildingInfoService.InsertUnit(3, 10, 150);
            Console.WriteLine(id);


            #region EF
            IEnumerable<Student> students = null;
            //using (var dbContext = new StudentDbContext())
            //{
            //    students = dbContext.Students.Where(x => x.Name == "Ali").ToList();
            //    /*OK=>  var dateTime = new DateTime(1900, 1, 1);
            //     * students = dbContext.Students.Where(x => x.BirthDate > dateTime).ToList();
            //    */
            //    // OK=> students = dbContext.Students.Where(x => x.BirthDate.Year > 1900).ToList();
            //    //Don't do this => students = dbContext.Students.Where(x=> IsBithYearGraterThan(x.BirthDate.Year,1900)).ToList();
            //}
            //foreach (var item in students)
            //{
            //    Console.WriteLine($"{item.Id}. {item.Name}");
            //}

            //using (var dbContext = new StudentDbContext())
            //{
            //    var s = new Student { Name = "Nima", BirthDate = new DateTime(2007, 08, 09) };
            //    dbContext.Students.Add(s);
            //    dbContext.SaveChanges();
            //}

            //using (var dbContext = new StudentDbContext())
            //{
            //    var s = dbContext.Students.FirstOrDefault(x => x.Id == 1);

            //    if (s != null)
            //    {
            //        s.Name = "Nima";
            //    }

            //    dbContext.SaveChanges();
            //}

            //using (var dbContext = new StudentDbContext())
            //{
            //    var s = new Student { Id = 1 };
            //    dbContext.Students.Remove(s);
            //    dbContext.SaveChanges();
            //}

            //var s = new Student { Name = "Nima", BirthDate = new DateTime(2007, 08, 09), Id = 2 };
            //using (var dbContext = new StudentDbContext())
            //{
            //    dbContext.Attach<Student>(s);
            //    s.Name = "Hosein";
            //    dbContext.SaveChanges();
            //}

            //var s = new Student { Id = 2};
            //using (var dbContext = new StudentDbContext())
            //{

            //    dbContext.Students.Remove(s);
            //    dbContext.SaveChanges();
            //}


            //using (var dbContext = new StudentDbContext())
            //{

            //    var s1 = new Student { Id = 7 };
            //    var s2 = new Student { Id = 3 };
            //    dbContext.Attach<Student>(s1);
            //    dbContext.Attach<Student>(s2);
            //    s1.BirthDate = new DateTime(2000, 1, 1);
            //    s2.BirthDate = new DateTime(2001, 1, 1);
            //    dbContext.SaveChanges();
            //}



            //using (var dbContext = new StudentDbContext())
            //{
            //    var s1 = new Student { Id = 7 };
            //    var s2 = new Student { Id = 3 };
            //    //dbContext.Students.RemoveRange(new Student[] { s1, s2 });
            //    dbContext.Students.RemoveRange(s1, s2);
            //    dbContext.SaveChanges();
            //}


            //Teacher t = null;

            //using (var dbContext = new StudentDbContext())
            //{
            //    //Eager Loading
            //    //    var teacher = dbContext.Teachers.Include(x => x.Students).FirstOrDefault();

            //    //Explicit Loading
            //    //var teacher = dbContext.Teachers.FirstOrDefault();
            //    //dbContext.Entry(teacher).Collection(x => x.Students).Load();

            //    //Lazy Loading
            //    //var teacher = dbContext.Teachers.FirstOrDefault();
            //    //Console.WriteLine(teacher.Students.Count);

            //    t = dbContext.Teachers.FirstOrDefault();
            //}

            //using (var dbContext = new StudentDbContext())
            //{
            //    var studentsList = dbContext.Students.ToList();
            //}


            //Console.WriteLine(t.Students.Count);
            #endregion EF

            #region params
            //var a = Add(1, 2, 3);
            //var b = Add(1);
            //var c = Add();
            //var d = Add(new int[] { 1,2,3,4,5});
            #endregion params

            #region Reflection
            var formulaNames = CalculationFormulaFactory.GetAll();
            var formula = CalculationFormulaFactory.Create(formulaNames[0].TypeName);
            var share = formula.Calculate(null, null, 1);
            #endregion Reflection
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static int Add(params int[] numbers)
        {
            int i = 0;
            if (numbers != null)
            {
                foreach (var item in numbers)
                {
                    i += item;
                }
            }
            return i;
        }
        public static bool IsBithYearGraterThan(int studentBirthYear, int year) => studentBirthYear > year;
        private static void DoMyFirstJob()
        {
            for (int i = 0; i < 100; i++)
            {
                autoResetEvent.WaitOne();
                Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId} This is item # {i}");
                autoResetEvent.Set();
            }
        }

        private static void DoMySecondJob()
        {
            autoResetEvent.Set();
            for (int i = 101; i < 200; i++)
            {
                autoResetEvent.Set();
                Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId}  =====> # {i}");
                autoResetEvent.WaitOne();
            }
        }
    }
}