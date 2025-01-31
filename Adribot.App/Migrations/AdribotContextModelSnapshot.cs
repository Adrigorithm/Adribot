﻿// <auto-generated />
using System;
using Adribot.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Adribot.Migrations
{
    [DbContext(typeof(AdribotContext))]
    partial class AdribotContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Adribot.Entities.Discord.DGuild", b =>
                {
                    b.Property<int>("DGuildId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DGuildId"));

                    b.Property<decimal>("GuildId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("StarEmojis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StarEmotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StarThreshold")
                        .HasColumnType("int");

                    b.Property<decimal?>("StarboardChannel")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("DGuildId");

                    b.ToTable("DGuilds");
                });

            modelBuilder.Entity("Adribot.Entities.Discord.DMember", b =>
                {
                    b.Property<int>("DMemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DMemberId"));

                    b.Property<int>("DGuildId")
                        .HasColumnType("int");

                    b.Property<decimal>("MemberId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("Mention")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DMemberId");

                    b.HasIndex("DGuildId");

                    b.ToTable("DMembers");
                });

            modelBuilder.Entity("Adribot.Entities.Discord.Infraction", b =>
                {
                    b.Property<int>("InfractionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InfractionId"));

                    b.Property<int>("DMemberId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("InfractionId");

                    b.HasIndex("DMemberId");

                    b.ToTable("Infractions");
                });

            modelBuilder.Entity("Adribot.Entities.Utilities.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("End")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("IcsCalendarId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAllDay")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Organiser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("Start")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventId");

                    b.HasIndex("IcsCalendarId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Adribot.Entities.Utilities.IcsCalendar", b =>
                {
                    b.Property<int>("IcsCalendarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IcsCalendarId"));

                    b.Property<decimal>("ChannelId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<int>("DMemberId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IcsCalendarId");

                    b.HasIndex("DMemberId");

                    b.ToTable("IcsCalendars");
                });

            modelBuilder.Entity("Adribot.Entities.Utilities.Reminder", b =>
                {
                    b.Property<int>("ReminderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReminderId"));

                    b.Property<decimal?>("Channel")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DMemberId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("ReminderId");

                    b.HasIndex("DMemberId");

                    b.ToTable("Reminders");
                });

            modelBuilder.Entity("Adribot.Entities.Utilities.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DMemberId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.HasIndex("DMemberId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Adribot.Entities.Discord.DMember", b =>
                {
                    b.HasOne("Adribot.Entities.Discord.DGuild", "DGuild")
                        .WithMany("Members")
                        .HasForeignKey("DGuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DGuild");
                });

            modelBuilder.Entity("Adribot.Entities.Discord.Infraction", b =>
                {
                    b.HasOne("Adribot.Entities.Discord.DMember", "DMember")
                        .WithMany("Infractions")
                        .HasForeignKey("DMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DMember");
                });

            modelBuilder.Entity("Adribot.Entities.Utilities.Event", b =>
                {
                    b.HasOne("Adribot.Entities.Utilities.IcsCalendar", "IcsCalendar")
                        .WithMany("Events")
                        .HasForeignKey("IcsCalendarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IcsCalendar");
                });

            modelBuilder.Entity("Adribot.Entities.Utilities.IcsCalendar", b =>
                {
                    b.HasOne("Adribot.Entities.Discord.DMember", "DMember")
                        .WithMany("Calendars")
                        .HasForeignKey("DMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DMember");
                });

            modelBuilder.Entity("Adribot.Entities.Utilities.Reminder", b =>
                {
                    b.HasOne("Adribot.Entities.Discord.DMember", "DMember")
                        .WithMany("Reminders")
                        .HasForeignKey("DMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DMember");
                });

            modelBuilder.Entity("Adribot.Entities.Utilities.Tag", b =>
                {
                    b.HasOne("Adribot.Entities.Discord.DMember", "DMember")
                        .WithMany("Tags")
                        .HasForeignKey("DMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DMember");
                });

            modelBuilder.Entity("Adribot.Entities.Discord.DGuild", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("Adribot.Entities.Discord.DMember", b =>
                {
                    b.Navigation("Calendars");

                    b.Navigation("Infractions");

                    b.Navigation("Reminders");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Adribot.Entities.Utilities.IcsCalendar", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
