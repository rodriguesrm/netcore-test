﻿using FluentValidator;
using Medical.Domain.Entities;
using Medical.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Medical.Infra.Data
{

    /// <summary>
    /// Database context
    /// </summary>
    public class MedicalDbContext : DbContext
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Microsoft.EntityFrameworkCore.DbContext class using the specified options
        /// </summary>
        /// <param name="options">The options for this context</param>
        public MedicalDbContext(DbContextOptions<MedicalDbContext> options) : base(options) { }

        #endregion

        #region Overrides

        /// <summary>
        /// Config model context
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder object instance</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Tables configs
            SetTableConfiguration(modelBuilder);

            // Entidades a serem ignoradas
            modelBuilder.Ignore<Notification>();

            base.OnModelCreating(modelBuilder);

        }

        /// <summary>
        /// Set tables (entities) configurations
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void SetTableConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
        }

        #endregion

        #region DbSets

        /// <summary>
        /// Doctorss entity collection
        /// </summary>
        public virtual DbSet<Doctor> Doctors { get; set; }

        /// <summary>
        /// Patients entity collection
        /// </summary>
        public virtual DbSet<Patient> Patients { get; set; }

        /// <summary>
        /// Appointments entity collection
        /// </summary>
        public virtual DbSet<Appointment> Appointments { get; set; }

        #endregion

    }

}
