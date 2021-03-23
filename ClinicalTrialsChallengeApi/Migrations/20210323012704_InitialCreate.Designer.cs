﻿// <auto-generated />
using System;
using ClinicalTrialsChallengeApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClinicalTrialsChallengeApi.Migrations
{
    [DbContext(typeof(ClinicalTrialsDbContext))]
    [Migration("20210323012704_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("ClinicalTrialsChallengeApi.Domain.Model.Notification.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Base64EncodedContent")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("EmailId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmailId");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("ClinicalTrialsChallengeApi.Domain.Model.Notification.Email", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<string>("RecipientAddress")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Sent")
                        .HasColumnType("TEXT");

                    b.Property<string>("Subject")
                        .HasColumnType("TEXT");

                    b.Property<string>("email_type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RecipientAddress");

                    b.ToTable("Emails");

                    b.HasDiscriminator<string>("email_type").HasValue("Email");
                });

            modelBuilder.Entity("ClinicalTrialsChallengeApi.Domain.Model.Notification.Recipient", b =>
                {
                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Address");

                    b.ToTable("Recipient");
                });

            modelBuilder.Entity("ClinicalTrialsChallengeApi.Domain.Model.Notification.ContactRequestEmail", b =>
                {
                    b.HasBaseType("ClinicalTrialsChallengeApi.Domain.Model.Notification.Email");

                    b.HasDiscriminator().HasValue("ContactRequestEmail");
                });

            modelBuilder.Entity("ClinicalTrialsChallengeApi.Domain.Model.Notification.StudyRequestEmail", b =>
                {
                    b.HasBaseType("ClinicalTrialsChallengeApi.Domain.Model.Notification.Email");

                    b.HasDiscriminator().HasValue("StudyRequestEmail");
                });

            modelBuilder.Entity("ClinicalTrialsChallengeApi.Domain.Model.Notification.Attachment", b =>
                {
                    b.HasOne("ClinicalTrialsChallengeApi.Domain.Model.Notification.Email", null)
                        .WithMany("Attachments")
                        .HasForeignKey("EmailId");
                });

            modelBuilder.Entity("ClinicalTrialsChallengeApi.Domain.Model.Notification.Email", b =>
                {
                    b.HasOne("ClinicalTrialsChallengeApi.Domain.Model.Notification.Recipient", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientAddress");

                    b.Navigation("Recipient");
                });

            modelBuilder.Entity("ClinicalTrialsChallengeApi.Domain.Model.Notification.Email", b =>
                {
                    b.Navigation("Attachments");
                });
#pragma warning restore 612, 618
        }
    }
}
