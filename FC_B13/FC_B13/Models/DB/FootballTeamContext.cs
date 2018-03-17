using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FC_B13.Models.DB
{
    public partial class FootballTeamContext : DbContext
    {
        public virtual DbSet<Coach> Coach { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Inventar> Inventar { get; set; }
        public virtual DbSet<Personal> Personal { get; set; }
        public virtual DbSet<PersonalProfession> PersonalProfession { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Profession> Profession { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TeamCoach> TeamCoach { get; set; }
        public virtual DbSet<User> User { get; set; }

        public FootballTeamContext(DbContextOptions<FootballTeamContext> options)
: base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coach>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirhday).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PositionInTeam)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.Coach)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Coaches_Contracts");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.ConclusionTime).HasColumnType("date");

                entity.Property(e => e.EndTime).HasColumnType("date");

                entity.Property(e => e.StartTime).HasColumnType("date");
            });

            modelBuilder.Entity<Inventar>(entity =>
            {
                entity.HasIndex(e => e.InventarName)
                    .HasName("nonclustered_index_name");

                entity.Property(e => e.InventarName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Inventar)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventar_Teams");
            });

            modelBuilder.Entity<Personal>(entity =>
            {
                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DataOfBirthday).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.Personal)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Personal_Contract");
            });

            modelBuilder.Entity<PersonalProfession>(entity =>
            {
                entity.HasKey(e => new { e.PersonalId, e.ProfessionId });

                entity.HasOne(d => d.Personal)
                    .WithMany(p => p.PersonalProfession)
                    .HasForeignKey(d => d.PersonalId)
                    .HasConstraintName("FK_PersonalAndProfession_Personal");

                entity.HasOne(d => d.Profession)
                    .WithMany(p => p.PersonalProfession)
                    .HasForeignKey(d => d.ProfessionId)
                    .HasConstraintName("FK_PersonalAndProfession_Professions");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Citizenship)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirhday).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleOnTheField)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.Player)
                    .HasForeignKey(d => d.ContractId)
                    .HasConstraintName("FK_Players_Contracts");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Player)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Players_Teams");
            });

            modelBuilder.Entity<Profession>(entity =>
            {
                entity.Property(e => e.ProfessionName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.NameTeam)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TeamCoach>(entity =>
            {
                entity.HasKey(e => new { e.TeamId, e.CoachId });

                entity.HasOne(d => d.Coach)
                    .WithMany(p => p.TeamCoach)
                    .HasForeignKey(d => d.CoachId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamsAndCoaches_Coaches");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamCoach)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamsAndCoaches_Teams");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });
        }
    }
}
