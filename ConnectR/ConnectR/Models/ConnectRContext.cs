namespace ConnectR.Models
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class ConnectRContext : DbContext
    {
        public ConnectRContext()
            : base("name=LocalConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Conference> Conferences { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}