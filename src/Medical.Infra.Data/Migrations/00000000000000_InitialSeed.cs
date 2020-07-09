using Medical.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Medical.Infra.Data.Migrations
{

    /// <summary>
    /// Initial seed data
    /// </summary>
    public abstract class InitialSeed : Migration
    {

        /// <summary>
        /// Seed data in database
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected void Seed(MigrationBuilder migrationBuilder)
        {

            Guid doctorId1 = new Guid("76D2B0F8-EB84-4CC4-87C0-780C7C3C79B7");
            Guid doctorId2 = new Guid("A5ECD70D-5486-40FD-91A4-18F5E88B22AA");
            Guid doctorId3 = new Guid("AF07A3FA-9CFA-4A33-801E-744269BBA4D8");

            Guid patientId1 = new Guid("515D7728-540F-4DF7-B7FA-2FE1D915059D");
            Guid patientId2 = new Guid("53A53E8A-F06A-4668-AC39-3CD344389564");
            Guid patientId3 = new Guid("D49051C4-ECC9-4893-B571-A104C34B450B");

            Guid appClient1Id = new Guid("daccabea-546c-49ee-989b-9e4e49611419");
            Guid appClient2Id = new Guid("ad4e2422-ea77-44ff-845e-122e03da9bbb");

            string appClient1Key = "TcwGjXfxk77CDNjnsSa35bFLKmvaLBWc";
            string appClient2Key = "B6TprSBX8cq85u653JAa2sxyR5c5WrLB";

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day + 1;

            migrationBuilder.Sql("exec sp_msforeachtable 'alter table ? nocheck constraint all'");

            // Application client seed
            migrationBuilder.InsertData
            (
                nameof(AppClient),
                new string[] { nameof(AppClient.Id), nameof(AppClient.Name), nameof(AppClient.Key) },
                new object[] { appClient1Id, "Company A", appClient1Key }
            );
            migrationBuilder.InsertData
            (
                nameof(AppClient),
                new string[] { nameof(AppClient.Id), nameof(AppClient.Name), nameof(AppClient.Key) },
                new object[] { appClient2Id, "Company B", appClient2Key }
            );

            // Doctor seed
            migrationBuilder.InsertData
            (
                nameof(Doctor),
                new string[] { nameof(Doctor.Id), nameof(Doctor.Crm), nameof(Doctor.FirstName), nameof(Doctor.LastName) },
                new object[] { doctorId1, "10001/2011", "STEVE", "ROGERS" }
            );
            migrationBuilder.InsertData
            (
                nameof(Doctor),
                new string[] { nameof(Doctor.Id), nameof(Doctor.Crm), nameof(Doctor.FirstName), nameof(Doctor.LastName) },
                new object[] { doctorId2, "20002/2012", "NATASHA", "ROMANOFF" }
            );
            migrationBuilder.InsertData
            (
                nameof(Doctor),
                new string[] { nameof(Doctor.Id), nameof(Doctor.Crm), nameof(Doctor.FirstName), nameof(Doctor.LastName) },
                new object[] { doctorId3, "20003/2013", "TONY", "STARK" }
            );

            // Patient seed
            migrationBuilder.InsertData
            (
                nameof(Patient),
                new string[] { nameof(Patient.Id), nameof(Patient.Cpf), nameof(Patient.FirstName), nameof(Patient.LastName) },
                new object[] { patientId1, "91886236020", "CLARK", "KENT" }
            );
            migrationBuilder.InsertData
            (
                nameof(Patient),
                new string[] { nameof(Patient.Id), nameof(Patient.Cpf), nameof(Patient.FirstName), nameof(Patient.LastName) },
                new object[] { patientId2, "82329541082", "BRUCE", "WAYNE" }
            );
            migrationBuilder.InsertData
            (
                nameof(Patient),
                new string[] { nameof(Patient.Id), nameof(Patient.Cpf), nameof(Patient.FirstName), nameof(Patient.LastName) },
                new object[] { patientId3, "50676340067", "DAIANA", "PRINCE" }
            );

            // Appointment
            migrationBuilder.InsertData
            (
                nameof(Appointment),
                new string[] { nameof(Appointment.Id), nameof(Appointment.DoctorId), nameof(Appointment.PatientId), nameof(Appointment.DateTime) },
                new object[] { Guid.NewGuid(), doctorId1, patientId2, new DateTime(year, month, day, 14, 30, 00) }
            );
            migrationBuilder.InsertData
            (
                nameof(Appointment),
                new string[] { nameof(Appointment.Id), nameof(Appointment.DoctorId), nameof(Appointment.PatientId), nameof(Appointment.DateTime) },
                new object[] { Guid.NewGuid(), doctorId1, patientId3, new DateTime(year, month, day, 15, 00, 00) }
            );
            migrationBuilder.InsertData
            (
                nameof(Appointment),
                new string[] { nameof(Appointment.Id), nameof(Appointment.DoctorId), nameof(Appointment.PatientId), nameof(Appointment.DateTime) },
                new object[] { Guid.NewGuid(), doctorId2, patientId1, new DateTime(year, month, day, 14, 00, 00) }
            );


            migrationBuilder.Sql("exec sp_msforeachtable 'alter table ? check constraint all'");


        }


    }

}
