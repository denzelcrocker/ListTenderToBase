using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static ListTenderToBase.CurrentList;

namespace ListTenderToBase {



    internal class Program {
        static void Main(string[] args) {
            Procurement procurement = new Procurement();
            Platform platform = new Platform();
            Organization organization = new Organization();
            TimeZone timeZone = new TimeZone();
            Method method = new Method();
            Act act = new Act();
            using (ApplicationContext db = new ApplicationContext()) {
                procurement.Number = ListSource.ListSource.listProcurement[0];
                procurement.Address = ListSource.ListSource.listProcurement[1];
                var methodElement = db.Methods.ToList();
                bool isMethodExists = true;
                foreach (Method u in methodElement) {
                    if (u.NameOfMethod == ListSource.ListSource.listProcurement[2]) {
                        procurement.MethodId = db.Methods.ToList().Where(x => x.NameOfMethod == ListSource.ListSource.listProcurement[2]).FirstOrDefault().MethodId;
                        isMethodExists = true;
                        break;
                    }
                    else {
                        isMethodExists = false;
                    }
                }
                if (isMethodExists == false) {
                    method.NameOfMethod = ListSource.ListSource.listProcurement[2];
                    db.Methods.AddRange(method);
                    db.SaveChanges();
                    procurement.MethodId = db.Methods.ToList().Where(x => x.NameOfMethod == ListSource.ListSource.listProcurement[2]).FirstOrDefault().MethodId;
                }
                var actElement = db.Acts.ToList();
                bool isActExists = true;
                foreach (Act u in actElement) {
                    if (u.NameOfAct == ListSource.ListSource.listProcurement[3]) {
                        procurement.ActId = db.Acts.ToList().Where(x => x.NameOfAct == ListSource.ListSource.listProcurement[3]).FirstOrDefault().ActId;
                        isActExists = true;
                        break;
                    }
                    else {
                        isActExists = false;
                    }
                }
                if (isActExists == false) {
                    act.NameOfAct = ListSource.ListSource.listProcurement[3];
                    db.Acts.AddRange(act);
                    db.SaveChanges();
                    procurement.ActId = db.Acts.ToList().Where(x => x.NameOfAct == ListSource.ListSource.listProcurement[3]).FirstOrDefault().ActId;
                }
                var platformElement = db.Platforms.ToList();
                bool isPlatformExists = true;
                foreach (Platform u in platformElement) {
                    if (u.NameOfPlatform == ListSource.ListSource.listProcurement[4] && u.AddressOfPlatform == ListSource.ListSource.listProcurement[5]) {
                        procurement.PlatformId = db.Platforms.ToList().Where(x => x.NameOfPlatform == ListSource.ListSource.listProcurement[4]).FirstOrDefault().PlatformId;
                        isPlatformExists = true;
                        break;
                    }
                    else {
                        isPlatformExists = false;
                    }
                }
                if (isPlatformExists == false) {
                    platform.NameOfPlatform = ListSource.ListSource.listProcurement[4];
                    platform.AddressOfPlatform = ListSource.ListSource.listProcurement[5];
                    db.Platforms.AddRange(platform);
                    db.SaveChanges();
                    procurement.PlatformId = db.Platforms.ToList().Where(x => x.NameOfPlatform == ListSource.ListSource.listProcurement[4]).FirstOrDefault().PlatformId;
                }
                var timeZoneElement = db.TimeZones.ToList();
                bool isTimeZoneExists = true;
                if (ListSource.ListSource.listProcurement[16] == "") {
                    ListSource.ListSource.listProcurement[16] = "0";
                }
                foreach (TimeZone u in timeZoneElement) {
                    if (u.Code == Convert.ToInt16(ListSource.ListSource.listProcurement[16])) {
                        procurement.TimeZoneId = db.TimeZones.ToList().Where(x => x.Code == Convert.ToInt16(ListSource.ListSource.listProcurement[16])).FirstOrDefault().TimeZoneId;
                        isTimeZoneExists = true;
                        break;
                    }
                    else {
                        isTimeZoneExists = false;
                    }
                }
                if (isTimeZoneExists == false) {
                    timeZone.Code = Convert.ToInt16(ListSource.ListSource.listProcurement[16]);
                    db.TimeZones.AddRange(timeZone);
                    db.SaveChanges();
                    procurement.TimeZoneId = db.TimeZones.ToList().Where(x => x.Code == Convert.ToInt16(ListSource.ListSource.listProcurement[16])).FirstOrDefault().TimeZoneId;
                }
                var organizationElement = db.Organizations.ToList();
                bool isOrganizationExists = true;
                foreach (Organization u in organizationElement) {
                    if (u.NameOfOrganization == ListSource.ListSource.listProcurement[9] && u.AddressOfOrganization == ListSource.ListSource.listProcurement[10]) {
                        procurement.OrganizationId = db.Organizations.ToList().Where(x => x.NameOfOrganization == ListSource.ListSource.listProcurement[9]).FirstOrDefault().OrganizationId;
                        isOrganizationExists = true;
                        break;
                    }
                    else {
                        isOrganizationExists = false;
                    }
                }
                if (isOrganizationExists == false) {
                    organization.NameOfOrganization = ListSource.ListSource.listProcurement[9];
                    organization.AddressOfOrganization = ListSource.ListSource.listProcurement[10];
                    db.Organizations.AddRange(organization);
                    db.SaveChanges();
                    procurement.OrganizationId = db.Organizations.ToList().Where(x => x.NameOfOrganization == ListSource.ListSource.listProcurement[9]).FirstOrDefault().OrganizationId;
                }
                procurement.DeadlineStart = Convert.ToDateTime(ListSource.ListSource.listProcurement[6]);
                procurement.DeadlineEnd = Convert.ToDateTime(ListSource.ListSource.listProcurement[7]);
                procurement.InitialPrice = Convert.ToDecimal(ListSource.ListSource.listProcurement[8]);
                procurement.ProcurementObject = ListSource.ListSource.listProcurement[11];
                procurement.PlaceOfDelivery = ListSource.ListSource.listProcurement[12];
                procurement.SupplyAssurance = ListSource.ListSource.listProcurement[13];
                procurement.Enforcement = ListSource.ListSource.listProcurement[14];
                procurement.ProvidingAguarantee = ListSource.ListSource.listProcurement[15];
                db.Procurements.AddRange(procurement);
                db.SaveChanges();
            }
            // получение данных
            using (ApplicationContext db = new ApplicationContext()) {
                // получаем объекты из бд и выводим на консоль
                var procurementElement = db.Procurements.ToList();
                Console.WriteLine("Procurements list:");
                foreach (Procurement u in procurementElement) {
                    Console.WriteLine($"Номер тендера: {u.Id}\n" +
                                      $"Номер на госзакупках: {u.Number}\n" +
                                      $"Адрес: {u.Address}\n" +
                                      $"Способ определения поставщика: {db.Methods.ToList().Where(x => x.MethodId == u.MethodId).FirstOrDefault().NameOfMethod}\n" +
                                      $"Закон: {db.Acts.ToList().Where(x => x.ActId == u.ActId).FirstOrDefault().NameOfAct}\n" +
                                      $"Наименование электронной площадки: {db.Platforms.ToList().Where(x => x.PlatformId == u.PlatformId).FirstOrDefault().NameOfPlatform}\n" +
                                      $"Ссылка на электронную площадку: {db.Platforms.ToList().Where(x => x.PlatformId == u.PlatformId).FirstOrDefault().AddressOfPlatform}\n" +
                                      $"Дата начала подачи заявок: {u.DeadlineStart:g} (МСК{db.TimeZones.ToList().Where(x => x.TimeZoneId == u.TimeZoneId).FirstOrDefault().Code})\n" +
                                      $"Дата окончания подачи заявок: {u.DeadlineEnd:g} (МСК{db.TimeZones.ToList().Where(x => x.TimeZoneId == u.TimeZoneId).FirstOrDefault().Code})\n" +
                                      $"Начальная цена: {u.InitialPrice}\n" +
                                      $"Организация, осуществляющая закупку: {db.Organizations.ToList().Where(x => x.OrganizationId == u.OrganizationId).FirstOrDefault().NameOfOrganization}\n" +
                                      $"Адрес организации, осуществляющей закупку: {db.Organizations.ToList().Where(x => x.OrganizationId == u.OrganizationId).FirstOrDefault().AddressOfOrganization}\n" +
                                      $"Объект закупки: {u.ProcurementObject}\n" +
                                      $"Место доставки товара: {u.PlaceOfDelivery}\n" +
                                      $"Обеспечение подачи заявки: {u.SupplyAssurance}\n" +
                                      $"Обеспечение исполнения заявки: {u.Enforcement}\n" +
                                      $"Обеспечение гарантии заявки: {u.ProvidingAguarantee}\n");
                }
            }
        }
    }
}