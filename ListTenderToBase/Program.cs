using Microsoft.EntityFrameworkCore;
namespace ListTenderToBase
{
    public class Procurement
    {
        public int ID { get; set; }
        public string? Number { get; set; }
        public string? Address { get; set; }
        public string? Method { get; set; }
        public string? Act { get; set; }
        public int PlatformId { get; set; }
        public DateTime? DeadlineStart { get; set; }
        public DateTime? DeadlineEnd { get; set; }
        public int TimeZoneId { get; set; }
        public decimal? InitialPrice { get; set; }
        public int OrganizationId { get; set; }
        public string? ProcurementObject { get; set; }
        public string? PlaceOfDelivery { get; set; }
        public string? SupplyAssurance { get; set; }
        public string? Enforcement { get; set; }
        public string? ProvidingAGuarantee { get; set; }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<Procurement> Procurements { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ngknn.ru;Database=BaseForGraduationProject;User ID = 33П; Password = 12357; TrustServerCertificate = true");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ListSource.ListSource source = new ListSource.ListSource();


            using (ApplicationContext db = new ApplicationContext())
            {
                Procurement procurement = new Procurement
                {
                    Number = source.listProcurement[0],
                    DeadlineEnd = Convert.ToDateTime("24.10.2003 18:34"), PlatformId = 1, InitialPrice = Convert.ToDecimal(34535.34)
                };

                // добавляем их в бд
                db.Procurements.AddRange(procurement);
                db.SaveChanges();
            }
            // получение данных
            using (ApplicationContext db = new ApplicationContext())
            {
                // получаем объекты из бд и выводим на консоль
                var users = db.Procurements.ToList();
                Console.WriteLine("Users list:");
                foreach (Procurement u in users)
                {
                    Console.WriteLine($"{u.ID}.{u.DeadlineEnd:g} - {u.PlatformId} - {u.Number}");
                }
            }
        }
    }
}