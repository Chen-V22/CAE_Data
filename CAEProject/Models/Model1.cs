namespace CAEProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        //---活動集錦Start---
        public virtual DbSet<ActivityPhoto> ActivityPhotos { get; set; }

        public virtual DbSet<ActivityRecord> ActivityRecords { get; set; }
        //---活動集錦End---

        public virtual DbSet<Ad> Ads { get; set; }

        public virtual DbSet<Faq> Faqs { get; set; }

        //---會員Start---
        public virtual DbSet<MbCaeSoftwareRecord> MbCaeSoftwareRecords { get; set; }

        public virtual DbSet<MbLevel> MbLevels { get; set; }

        public virtual DbSet<MbPaid> MbPaids { get; set; }

        public virtual DbSet<MbPoints> MbPointses { get; set; }

        public virtual DbSet<MbRemarks> MbRemarkses { get; set; }

        public virtual DbSet<Member> Members { get; set; }
        //---會員End---

        public System.Data.Entity.DbSet<CAEProject.Models.News> News { get; set; }

        public virtual DbSet<Newsletter> Newsletters { get; set; }

        public virtual DbSet<Premission> Premissions { get; set; }

        public virtual DbSet<ProjectService> ProjectServices { get; set; }

        public virtual DbSet<PsReply> PsReplies { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<RegistrationMinor> RegistrationMinors { get; set; }

        public virtual DbSet<RegistrationForm> RegistrationForms { get; set; }

        public virtual DbSet<ResultsPublished> ResultsPublisheds { get; set; }

        public virtual DbSet<Seminar> Seminars { get; set; }

        public virtual DbSet<ShowOff> ShowOffs { get; set; }

        public virtual DbSet<Site> Sites { get; set; }

        public virtual DbSet<TechnicalKnowledge> TechnicalKnowledges { get; set; }

        public virtual DbSet<TrainingCourse> TrainingCourses { get; set; }

        public virtual DbSet<User> Users { get; set; }

        //---ViewModel---

        //public System.Data.Entity.DbSet<CAEProject.Models.MbPaidViewModel> MbPaidViewModels { get; set; }

        //public System.Data.Entity.DbSet<CAEProject.Models.MbFreeViewModel> MbFreeViewModels { get; set; }

        //public System.Data.Entity.DbSet<CAEProject.Models.ConnectionUsViewModel> ConnectionUsViewModel { get; set; }

        // ------------------
    }
}
