﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Simplifly.Context;

#nullable disable

namespace Simplifly.Migrations
{
    [DbContext(typeof(RequestTrackerContext))]
    [Migration("20240613082305_seatDetailsChanged")]
    partial class seatDetailsChanged
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Simplifly.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AdminId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Simplifly.Models.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("Simplifly.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BookingTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("bookingStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Simplifly.Models.CancelledBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("RefundAmount")
                        .HasColumnType("float");

                    b.Property<string>("RefundStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("cardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("passengerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("paymentId")
                        .HasColumnType("int");

                    b.Property<int>("scheduleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("paymentId");

                    b.HasIndex("scheduleId");

                    b.ToTable("CancelledBookings");
                });

            modelBuilder.Entity("Simplifly.Models.Customer", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Simplifly.Models.Flight", b =>
                {
                    b.Property<string>("FlightNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Airline")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("BasePrice")
                        .HasColumnType("float");

                    b.Property<double>("BusinessClassSeatPrice")
                        .HasColumnType("float");

                    b.Property<double>("EconomySeatPrice")
                        .HasColumnType("float");

                    b.Property<int>("FlightOwnerOwnerId")
                        .HasColumnType("int");

                    b.Property<double>("PremiumEconomySeatPrice")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TotalBusinessClassSeats")
                        .HasColumnType("int");

                    b.Property<int>("TotalEconomySeats")
                        .HasColumnType("int");

                    b.Property<int>("TotalPremiumEconomySeats")
                        .HasColumnType("int");

                    b.HasKey("FlightNumber");

                    b.HasIndex("FlightOwnerOwnerId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Simplifly.Models.FlightOwner", b =>
                {
                    b.Property<int>("OwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OwnerId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessRegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OwnerId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("FlightsOwners");
                });

            modelBuilder.Entity("Simplifly.Models.Passenger", b =>
                {
                    b.Property<int>("PassengerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PassengerId"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassportNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PassengerId");

                    b.HasIndex("UserId");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("Simplifly.Models.PassengerBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int?>("PassengerId")
                        .HasColumnType("int");

                    b.Property<string>("SeatNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("PassengerId");

                    b.HasIndex("SeatNumber");

                    b.ToTable("PassengerBookings");
                });

            modelBuilder.Entity("Simplifly.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentDetailsId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("PaymentDetailsId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Simplifly.Models.PaymentDetails", b =>
                {
                    b.Property<int>("PaymentDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentDetailsId"), 1L, 1);

                    b.Property<string>("CVV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpiryDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentDetailsId");

                    b.ToTable("PaymentDetails");
                });

            modelBuilder.Entity("Simplifly.Models.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DestinationAirportId")
                        .HasColumnType("int");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<int>("SourceAirportId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DestinationAirportId");

                    b.HasIndex("SourceAirportId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("Simplifly.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Arrival")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Departure")
                        .HasColumnType("datetime2");

                    b.Property<string>("FlightId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.HasIndex("RouteId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Simplifly.Models.SeatDetail", b =>
                {
                    b.Property<string>("SeatNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SeatClass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SeatNumber");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("Simplifly.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("Key")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Simplifly.Models.Admin", b =>
                {
                    b.HasOne("Simplifly.Models.User", "User")
                        .WithOne("admin")
                        .HasForeignKey("Simplifly.Models.Admin", "Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Simplifly.Models.Booking", b =>
                {
                    b.HasOne("Simplifly.Models.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Simplifly.Models.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("Simplifly.Models.CancelledBooking", b =>
                {
                    b.HasOne("Simplifly.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Simplifly.Models.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("paymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Simplifly.Models.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("scheduleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Payment");

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("Simplifly.Models.Customer", b =>
                {
                    b.HasOne("Simplifly.Models.User", "User")
                        .WithOne("customer")
                        .HasForeignKey("Simplifly.Models.Customer", "Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Simplifly.Models.Flight", b =>
                {
                    b.HasOne("Simplifly.Models.FlightOwner", "FlightOwner")
                        .WithMany("OwnedFlights")
                        .HasForeignKey("FlightOwnerOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FlightOwner");
                });

            modelBuilder.Entity("Simplifly.Models.FlightOwner", b =>
                {
                    b.HasOne("Simplifly.Models.User", "User")
                        .WithOne("flightOwner")
                        .HasForeignKey("Simplifly.Models.FlightOwner", "Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Simplifly.Models.Passenger", b =>
                {
                    b.HasOne("Simplifly.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Simplifly.Models.PassengerBooking", b =>
                {
                    b.HasOne("Simplifly.Models.Booking", "Booking")
                        .WithMany()
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Simplifly.Models.Passenger", "Passenger")
                        .WithMany()
                        .HasForeignKey("PassengerId");

                    b.HasOne("Simplifly.Models.SeatDetail", "SeatDetail")
                        .WithMany()
                        .HasForeignKey("SeatNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Passenger");

                    b.Navigation("SeatDetail");
                });

            modelBuilder.Entity("Simplifly.Models.Payment", b =>
                {
                    b.HasOne("Simplifly.Models.PaymentDetails", "PaymentDetails")
                        .WithMany()
                        .HasForeignKey("PaymentDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentDetails");
                });

            modelBuilder.Entity("Simplifly.Models.Route", b =>
                {
                    b.HasOne("Simplifly.Models.Airport", "DestinationAirport")
                        .WithMany()
                        .HasForeignKey("DestinationAirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Simplifly.Models.Airport", "SourceAirport")
                        .WithMany()
                        .HasForeignKey("SourceAirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DestinationAirport");

                    b.Navigation("SourceAirport");
                });

            modelBuilder.Entity("Simplifly.Models.Schedule", b =>
                {
                    b.HasOne("Simplifly.Models.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Simplifly.Models.Route", "Route")
                        .WithMany("Schedules")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Simplifly.Models.FlightOwner", b =>
                {
                    b.Navigation("OwnedFlights");
                });

            modelBuilder.Entity("Simplifly.Models.Route", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("Simplifly.Models.User", b =>
                {
                    b.Navigation("admin")
                        .IsRequired();

                    b.Navigation("customer")
                        .IsRequired();

                    b.Navigation("flightOwner")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
