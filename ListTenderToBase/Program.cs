using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ListTenderToBase
{
    //public class Platforms
    //{ 
    //    public int PlatformId { get; set; }
    //    public string? NameOfPlatform { get; set; }
    //    public string? AddressOfPlatform { get; set; }
    //    public List<Procurement> Procurement { get; set; }
    //}
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
                    Address = source.listProcurement[1],
                    Method = source.listProcurement[2],
                    Act = source.listProcurement[3],
                    PlatformId = 1,
                    TimeZoneId = 1,
                    OrganizationId = 1,
                    DeadlineStart = Convert.ToDateTime(source.listProcurement[6]),
                    DeadlineEnd = Convert.ToDateTime(source.listProcurement[7]),
                    InitialPrice = Convert.ToDecimal(source.listProcurement[8]),
                    ProcurementObject = source.listProcurement[11],
                    PlaceOfDelivery = source.listProcurement[12],
                    SupplyAssurance = source.listProcurement[13],
                    Enforcement = source.listProcurement[14],
                    ProvidingAGuarantee = source.listProcurement[15]
                };
                db.Procurements.AddRange(procurement);
                db.SaveChanges();
            }
            // получение данных
            using (ApplicationContext db = new ApplicationContext())
            {
                // получаем объекты из бд и выводим на консоль
                var items = db.Procurements.ToList();
                Console.WriteLine("Procurements list:");
                foreach (Procurement u in items)
                {
                    Console.WriteLine($"Номер тендера: {u.ID}\n" +
                                      $"Номер на госзакупках: {u.Number}\n" +
                                      $"Адрес: {u.Address}\n" +
                                      $"Способ определения поставщика: {u.Method}\n" +
                                      $"Закон: {u.Act}\n" +
                                      $"Наименование электронной площадки: \n" +
                                      $"Ссылка на электронную площадку: \n" +
                                      $"Дата начала подачи заявок: {u.DeadlineStart:g}\n" +
                                      $"Дата окончания подачи заявок: {u.DeadlineEnd:g}\n" +
                                      $"Начальная цена: {u.InitialPrice}\n" +
                                      $"Организация, осуществляющая закупку: \n" +
                                      $"Адрес организации, осуществляющей закупку: \n" +
                                      $"Объект закупки: {u.ProcurementObject}\n" +
                                      $"Место доставки товара: {u.PlaceOfDelivery}\n" +
                                      $"Обеспечение подачи заявки: {u.SupplyAssurance}\n" +
                                      $"Обеспечение исполнения заявки: {u.Enforcement}\n" +
                                      $"Обеспечение гарантии заявки: {u.ProvidingAGuarantee}\n");
                }
            }
        }
    }
}