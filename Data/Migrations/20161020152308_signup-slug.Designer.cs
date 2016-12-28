using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DYV.Data;

namespace DYV.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161020152308_signup-slug")]
    partial class signupslug
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DYV.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<bool>("QRCodeEnabled");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ApplicationUser");
                });

            modelBuilder.Entity("DYV.Models.ClientPractices", b =>
                {
                    b.Property<string>("ClientUserId");

                    b.Property<int>("PracticeId");

                    b.HasKey("ClientUserId", "PracticeId");

                    b.HasIndex("ClientUserId");

                    b.HasIndex("PracticeId");

                    b.ToTable("ClientPractices");
                });

            modelBuilder.Entity("DYV.Models.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1")
                        .IsRequired();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("ClientUserId")
                        .IsRequired();

                    b.Property<string>("Country");

                    b.Property<string>("County")
                        .IsRequired();

                    b.Property<DateTime>("DateAdded");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(12,9)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(12,9)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 60);

                    b.Property<string>("PostCode");

                    b.HasKey("Id");

                    b.HasIndex("ClientUserId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("DYV.Models.PlaceDayList", b =>
                {
                    b.Property<int>("PlaceId");

                    b.Property<int>("DayListId");

                    b.Property<int>("ZoomLevel");

                    b.HasKey("PlaceId", "DayListId");

                    b.HasIndex("DayListId");

                    b.HasIndex("PlaceId");

                    b.ToTable("PlaceDayLists");
                });

            modelBuilder.Entity("DYV.Models.Practice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("SignupSlug");

                    b.HasKey("Id");

                    b.ToTable("Practices");
                });

            modelBuilder.Entity("DYV.Models.SubscriberDashboard.DayList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Name");

                    b.Property<string>("SubscriberUserId");

                    b.HasKey("Id");

                    b.HasIndex("SubscriberUserId");

                    b.ToTable("DayLists");
                });

            modelBuilder.Entity("DYV.Models.SubscriberPractice", b =>
                {
                    b.Property<int>("PracticeId");

                    b.Property<string>("SubscriberUserId");

                    b.Property<bool>("IsManager");

                    b.HasKey("PracticeId", "SubscriberUserId");

                    b.HasIndex("PracticeId");

                    b.HasIndex("SubscriberUserId");

                    b.ToTable("SubscriberPractices");
                });

            modelBuilder.Entity("DYV.Models.UserInvite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email");

                    b.Property<int>("PracticeId");

                    b.Property<bool>("Used");

                    b.HasKey("Id");

                    b.ToTable("UserInvites");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DYV.Models.User.ClientUser", b =>
                {
                    b.HasBaseType("DYV.Models.ApplicationUser");


                    b.ToTable("ClientUser");

                    b.HasDiscriminator().HasValue("ClientUser");
                });

            modelBuilder.Entity("DYV.Models.User.SubscriberUser", b =>
                {
                    b.HasBaseType("DYV.Models.ApplicationUser");


                    b.ToTable("SubscriberUser");

                    b.HasDiscriminator().HasValue("SubscriberUser");
                });

            modelBuilder.Entity("DYV.Models.ClientPractices", b =>
                {
                    b.HasOne("DYV.Models.User.ClientUser", "ClientUser")
                        .WithMany("PermittedPractices")
                        .HasForeignKey("ClientUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DYV.Models.Practice", "Practice")
                        .WithMany("ClientPractices")
                        .HasForeignKey("PracticeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DYV.Models.Place", b =>
                {
                    b.HasOne("DYV.Models.User.ClientUser", "ClientUser")
                        .WithMany("Places")
                        .HasForeignKey("ClientUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DYV.Models.PlaceDayList", b =>
                {
                    b.HasOne("DYV.Models.SubscriberDashboard.DayList", "DayList")
                        .WithMany("PlaceDayLists")
                        .HasForeignKey("DayListId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DYV.Models.Place", "Place")
                        .WithMany()
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DYV.Models.SubscriberDashboard.DayList", b =>
                {
                    b.HasOne("DYV.Models.User.SubscriberUser", "SubscriberUser")
                        .WithMany("DayLists")
                        .HasForeignKey("SubscriberUserId");
                });

            modelBuilder.Entity("DYV.Models.SubscriberPractice", b =>
                {
                    b.HasOne("DYV.Models.Practice", "Practice")
                        .WithMany("SubscriberPractices")
                        .HasForeignKey("PracticeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DYV.Models.User.SubscriberUser", "SubscriberUser")
                        .WithMany("SubscriberPractices")
                        .HasForeignKey("SubscriberUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DYV.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DYV.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DYV.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
