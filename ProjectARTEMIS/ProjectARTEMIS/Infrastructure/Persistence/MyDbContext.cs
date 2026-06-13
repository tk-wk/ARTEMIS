using Microsoft.EntityFrameworkCore;
using ProjectARTEMIS.Domain.Enums;

namespace ProjectARTEMIS.Infrastructure.Persistence
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<School> Schools { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<PlayerProfile> PlayerProfiles { get; set; }
        public DbSet<PlayerOnlineStatus> PlayerOnlineStatuses { get; set; }
        public DbSet<PlayerProfileStatus> PlayerProfileStatuses { get; set; }
        public DbSet<SocialMediaHandle> SocialMediaHandles { get; set; }


        public DbSet<WhitelistRequest> WhitelistRequests { get; set; }
        public DbSet<WhitelistRequestStatus> WhitelistRequestStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WhitelistRequestStatus>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id)
                       .ValueGeneratedNever();
            });

            var adminUserId = new Guid("a1154c1d-8ee3-49dc-8a71-6c2306d8717c");
            var profileId = new Guid("b2265d2e-9ff4-50ed-9b82-7d3417e9828d");
            var schoolId = new Guid("44444444-4444-4444-4444-444444444444");

            string staticHashedPassword = "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy";

            modelBuilder.Entity<User>().HasData(new
            {
                Id = adminUserId,
                Username = "Spinelly",
                PasswordHash = staticHashedPassword,
                IsAdmin = true
            });

            modelBuilder.Entity<PlayerProfile>().HasData(new
            {
                Id = profileId,
                UserId = adminUserId,
                SchoolId = schoolId,
                RealName = "Alex Sam Cabildo",
                Bio = "Project ARTEMIS Head System Administrator.",
                Note = string.Empty,
                ProfilePicturePath = string.Empty
            });

            modelBuilder.Entity<PlayerProfileStatus>().HasData(new
            {
                Id = new Guid("c3376e3f-0aa5-61fe-ac93-8e4528fa939e"),
                PlayerProfileId = profileId,
                Status = PlayerStatusType.Active,
                Message = "Profile initialized.",
                StartTime = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });

            modelBuilder.Entity<PlayerOnlineStatus>().HasData(new
            {
                Id = new Guid("d4487f4a-1bb6-72fa-bd04-9f5639fb04af"),
                PlayerProfileId = profileId,
                Status = OnlineStatusType.Offline,
                StartTime = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });

            modelBuilder.Entity<School>().HasData(
                new
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "University of San Carlos (USC)",
                    Description = "One of the oldest and most prestigious universities in Cebu, known for its rigorous academic programs and sprawling campuses.",
                    ColorCode = "#006633"
                },
                new
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Cebu Institute of Technology – University (CIT-U)",
                    Description = "Renowned for its excellence in engineering, technology, and producing top-notch board exam placers (Home of the Technologians).",
                    ColorCode = "#800000"
                },
                new
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "University of San Jose – Recoletos (USJ-R)",
                    Description = "A highly respected Catholic institution known for outstanding business, IT, and engineering courses.",
                    ColorCode = "#003366"
                },
                new
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Name = "University of the Philippines Cebu (UP Cebu)",
                    Description = "The premier state university in the region, highly recognized for its competitive arts, design, and computer science programs.",
                    ColorCode = "#7B1113"
                },
                new
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Name = "Cebu Doctors' University (CDU)",
                    Description = "A top-tier medical and health sciences institution located near North Reclamation Area, producing world-class medical professionals.",
                    ColorCode = "#FFFFFF"
                }
            );

            modelBuilder.Entity<SocialMedia>().HasData(
            new
            {
                Id = Guid.Parse("f1198a5b-2cc7-83fa-cd05-8f674afb05bf"),
                Name = "Facebook"
            },
            new
            {
                Id = Guid.Parse("f22a9b6c-3dd8-94fa-de06-9f785b0c16cf"),
                Name = "X (Twitter)"
            },
            new
            {
                Id = Guid.Parse("f33bc77d-4ee9-05fa-ef07-af896c1d27df"),
                Name = "Discord"
            },
            new
            {
                Id = Guid.Parse("f44cd88e-5ff0-16fa-f008-bf9a7d2e38ef"),
                Name = "Instagram"
            },
            new
            {
                Id = Guid.Parse("f55de99f-6fa1-27fa-f109-cfab8e3f49ff"),
                Name = "TikTok"
            }
        );
        }

    }
}
