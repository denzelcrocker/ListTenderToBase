using System.Numerics;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;

namespace ListSource {
    public class ListSource {
        public static RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
        public List<string> listProcurement = new List<string>()
        {
            "0853500000323000731",//номер на госзакупках
            "https://zakupki.gov.ru/epz/order/notice/ea20/view/common-info.html?regNumber=0853500000323000731",//ссылка на тендер
            "Электронный аукцион",//способ определения поставщика
            "43-ФЗ",//закон
            "АО «Сбербанк-АСТ»",//наименование электронной площадки
            "http://www.sberbank-ast.ru",//ссылка на электронную площадку
            "14.02.2023 15:18",//дата начала подачи заявок
            "22.02.2023 09:00",//дата окончания подачи заявок
            "1 238 692,00",//начальная цена
            "ГОСУДАРСТВЕННОЕ КАЗЕННОЕ УЧРЕЖДЕНИЕ ОРЕНБУРГСКОЙ ОБЛАСТИ ЦЕНТР ОРГАНИЗАЦИИ ЗАКУПОК",//организация, осуществляющая закупку
            "460006, ОРЕНБУРГСКАЯ ОБЛАСТЬ, ГОРОД ОРЕНБУРГ, УЛИЦА КОМСОМОЛЬСКАЯ, ДОМ 122",//адрес организации, осуществляющей закупку
            "оказание услуг по техническому обслуживанию комплексов обработки избирательных бюллетеней КОИБ-2017",//объект закупки
            "460006, ОРЕНБУРГСКАЯ ОБЛАСТЬ, ГОРОД ОРЕНБУРГ, УЛИЦА КОМСОМОЛЬСКАЯ, ДОМ 122",//место доставки товара
            "",//обеспечение подачи
            "",//обеспечение исполнения
            "",//обеспечение гарантии
            "-2"//часовой пояс
        };

        public ListSource() {
            byte[] bytes;

            while (true) {
                try {
                    using HttpClient httpClient = new();
                    bytes = httpClient.GetByteArrayAsync("https://zakupki.gov.ru/epz/order/notice/ea20/view/common-info.html?regNumber=0338300012023000007").Result;

                    break;
                }
                catch (Exception) { }
            }

            string input = Encoding.UTF8.GetString(bytes);

            listProcurement[0] = Number.GetString(input);
            listProcurement[1] = "https://zakupki.gov.ru/epz/order/notice/ea20/view/common-info.html?regNumber=0338300012023000007";
            listProcurement[2] = Identifying.GetString(input);
            listProcurement[3] = Law.GetString(input);
            listProcurement[4] = Place.GetString(input);
            listProcurement[5] = PlaceUrl.GetString(input);
            listProcurement[6] = DateStart.GetString(input);
            listProcurement[7] = DateEnd.GetString(input);
            listProcurement[8] = Cost.GetString(input);
            listProcurement[9] = Requester.GetString(input);
            listProcurement[10] = PostAddress.GetString(input);
            listProcurement[11] = Objects.GetString(input);
            listProcurement[12] = Location.GetString(input);
            listProcurement[13] = "";
            listProcurement[14] = "";
            listProcurement[15] = "-2";
        }


        public sealed class Law {
            static readonly Regex regex = new(@"<div class=""cardMainInfo__title d-flex text-truncate""\n(?<space>.*?)\n(?<space>.*?)>(?<val>.*?)\n(?<space>.*?)</div>", options);

            public static string GetString(string input) {
                string value;
                value = regex.Split(input)[2];
                return value;
            }
        }

        public sealed class Identifying {
            static readonly Regex regex = new(@"<span class=""cardMainInfo__title distancedText ml-1"">\n                            (?<val>.*?)\n(?<space>.*?)</span>", options);

            public static string GetString(string input) {
                string value;
                value = regex.Split(input)[1];
                return value;
            }
        }

        public sealed class Number {
            static readonly Regex regex = new(@"<a href=""#"" target=""_blank"">№ (?<val>.*?)</a>", options);

            public static string GetString(string input) {
                string value;
                value = regex.Split(input)[1];
                return value;
            }
        }

        public sealed class Objects {
            static readonly Regex regex = new(@"<span class=""cardMainInfo__content"">(?<val>.*?)</span>", options);

            public static string GetString(string input) {
                string value;
                value = regex.Split(input)[1];
                return value;
            }
        }

        public sealed class Requester {
            static readonly Regex regex = new(@"<a href=""https://zakupki.gov.ru/epz/organization(?<space>.*?)"" target=""_blank"">(?<val>.*?)</a>", options);

            public static string GetString(string input) {
                string value;
                value = regex.Split(input)[2];
                return value;
            }
        }

        public sealed class Cost {
            static readonly Regex regex = new(@"<span class=""cardMainInfo__content cost"">\n                        (?<val>.*?) &#8381;", options);

            public static string GetString(string input) {
                string value;
                value = regex.Split(input)[1];
                return value;
            }
        }

        public sealed class Place {
            static readonly Regex regex = new(@"Наименование электронной площадки(?<space>.*?)</span>(?<space>.*?)>(?<val>.*?)<", options);

            public static string GetString(string input) {
                string value;
                value = regex.Split(input)[2];
                return value;
            }
        }

        public sealed class PlaceUrl {
            static readonly Regex regex = new(@"Адрес электронной площадки(?<space>.*?)</span>(?<space>.*?)href=""(?<val>.*?)""", options);

            public static string GetString(string input) {
                string value;
                value = regex.Split(input)[2];
                return value;
            }
        }

        public sealed class PostAddress {
            static readonly Regex regex = new(@"Почтовый адрес</span>(?<space>.*?)\n                    (?<val>.*?)\n", options);

            public static string GetString(string input) {
                string value;
                value = regex.Split(input)[2];
                return value;
            }
        }

        public sealed class Location {
            static readonly Regex regex = new(@"Место нахождения</span>(?<space>.*?)\n                    (?<val>.*?)\n", options);

            public static string GetString(string input) {
                string value;
                value = regex.Split(input)[2];
                return value;
            }
        }

        public sealed class Responsible {
            static readonly Regex regex = new(@"Ответственное должностное лицо</span>(?<space>.*?)\n                    (?<val>.*?)<", options);

            public static string GetString(string input) {
                string value;
                value = regex.Split(input)[2];
                return value;
            }
        }

        public sealed class Email {
            static readonly Regex regex = new(@"Адрес электронной почты</span>(?<space>.*?)                        (?<val>.*?)\n", options);

            public static string GetString(string input) {
                string value;
                value = regex.Split(input)[2];
                return value;
            }
        }

        public sealed class Phone {
            static readonly Regex regex = new(@"Номер контактного телефона</span>(?<space>.*?)                    (?<val>.*?)\n", options);

            public static string GetString(string input) {
                string value;
                value = regex.Split(input)[2];
                return value;
            }
        }

        public sealed class Fax {
            static readonly Regex regex = new(@"Факс</span>(?<space>.*?)>(?<val>.*?)<", options);

            public static string GetString(string input) {
                string value;
                value = regex.Split(input)[2];
                return value;
            }
        }

        public sealed class DateStart {
            static readonly Regex regex = new(@"Дата и время начала срока подачи заявок</span>(?<space>.*?)\n                    (?<valFirst>.*?) <span(?<space>.*?)>(?<valSecond>.*?)</span>", options);

            public static string GetString(string input) {
                string value;
                value = $"{regex.Split(input)[2]} {regex.Split(input)[3]}";
                return value;
            }
        }

        public sealed class DateEnd {
            static readonly Regex regex = new(@"Дата и время окончания срока подачи заявок</span>(?<space>.*?)\n                    (?<valFirst>.*?) <span(?<space>.*?)>(?<valSecond>.*?)</span>", options);

            public static string GetString(string input) {
                string value;
                value = $"{regex.Split(input)[2]} {regex.Split(input)[3]}";
                return value;
            }
        }
    }
}
