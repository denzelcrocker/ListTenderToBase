namespace ListSource {
    public class ListSource : List<string> {
        public static RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
        public static List<string> listProcurement = new()
        {
            "—",//номер на госзакупках
            "—",//ссылка на тендер
            "—",//способ определения поставщика
            "—",//закон
            "—",//наименование электронной площадки
            "—",//ссылка на электронную площадку
            "—",//дата начала подачи заявок
            "—",//дата окончания подачи заявок
            "—",//начальная цена
            "—",//организация, осуществляющая закупку
            "—",//адрес организации, осуществляющей закупку
            "—",//объект закупки
            "—",//место доставки товара
            "—",//обеспечение подачи
            "—",//обеспечение исполнения
            "—",//обеспечение гарантии
            "—"//часовой пояс
        };

        public ListSource() {
            byte[] bytes;

            while (true) {
                try {
                    using HttpClient httpClient = new();
                    bytes = httpClient.GetByteArrayAsync("https://zakupki.gov.ru/epz/order/notice/ea20/view/common-info.html?regNumber=0332300185023000007").Result;

                    break;
                }
                catch (Exception) { }
            }

            string input = Encoding.UTF8.GetString(bytes);

            listProcurement[0] = Number.GetString(input);
            listProcurement[1] = "https://zakupki.gov.ru/epz/order/notice/ea20/view/common-info.html?regNumber=0817200000323001437";
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
            listProcurement[13] = SupplyAssurance.GetString(input);
            listProcurement[14] = Security.GetString(input);
            listProcurement[15] = ProvidingAguarantee.GetString(input);
            listProcurement[16] = UTC.GetString(input);

            AddRange(listProcurement);
        }


        public sealed class Number {
            private static readonly Regex regex = new(@">№ (?<val>.*?)<", options);

            public static string GetString(string input) {
                string value = "—";
                try {
                    value = $"{regex.Split(input)[1]}";
                }
                catch { }
                return value;
            }
        }

        public sealed class Identifying {
            private static readonly Regex regex44 = new(@"Способ(?<space>.*?)\n(?<space>.*?)info"">(?<val>.*?)<", options);
            private static readonly Regex regex223 = new(@"Способ(?<space>.*?)\n(?<space>.*?)\n *(?<val>.*?)\n", options);

            public static string GetString(string input) {
                string value = "—";
                try {
                    value = regex44.Split(input)[2];
                }
                catch {
                    value = regex223.Split(input)[2];
                }
                return value;
            }
        }

        public sealed class Law {
            private static readonly Regex regex44 = new(@"<div class=""cardMainInfo__title d-flex text-truncate""\n(?<space>.*?)\n(?<space>.*?)>(?<val>.*?)\n(?<space>.*?)</div>", options);
            private static readonly Regex regex223 = new(@"<div class=""registry-entry__header-top__title"">\n *(?<val>.*?) ", options);

            public static string GetString(string input) {
                string value = "—";
                try {
                    value = regex44.Split(input)[2];
                }
                catch {
                    value = regex223.Split(input)[1];
                }
                value = value.Replace("&#034;", "\"");
                return value;
            }
        }

        public sealed class Place {
            private static readonly Regex regex44 = new(@"Наименование электронной площадки(?<space>.*?)>\n(?<space>.*?)info"">(?<val>.*?)</", options);
            private static readonly Regex regex223 = new(@"Наименование электронной площадки(?<space>.*?)>\n(?<space>.*?)>\n *(?<val>.*?)\n", options);

            public static string GetString(string input) {
                string value = "—";
                if (listProcurement[3] == "44-ФЗ") {
                    value = regex44.Split(input)[2];
                }
                else {
                    value = regex223.Split(input)[2];
                }
                return value;
            }
        }

        public sealed class PlaceUrl {
            private static readonly Regex regex44 = new(@"Адрес электронной площадки(?<space>.*?)</span>(?<space>.*?)href=""(?<val>.*?)""", options);
            private static readonly Regex regex223 = new(@"Адрес электронной площадки(?<space>.*?)>\n(?<space>.*?)>\n(?<space>.*?)href=""(?<val>.*?)""", options);

            public static string GetString(string input) {
                string value = "—";
                if (listProcurement[3] == "44-ФЗ") {
                    value = regex44.Split(input)[2];
                }
                else {
                    value = regex223.Split(input)[2];
                }
                return value;
            }
        }

        public sealed class DateStart {
            private static readonly Regex regex = new(@"начала(?<space>.*?)>\n(?<space>.*?)\n *(?<val>..\...\..... ..:..)", options);

            public static string GetString(string input) {
                string value = "—";
                try {
                    value = $"{regex.Split(input)[2]}";
                }
                catch { }
                return value;
            }
        }

        public sealed class DateEnd {
            private static readonly Regex regex = new(@"окончания(?<space>.*?)>\n(?<space>.*?)\n *(?<val>..\...\..... ..:..)", options);

            public static string GetString(string input) {
                string value = "—";
                try {
                    value = $"{regex.Split(input)[2]}";
                }
                catch { }
                return value;
            }
        }

        public sealed class Cost {
            private static readonly Regex regex = new(@"Начальная цена(?<space>.*?)>\n(?<space>.*?)>\n *(?<val>.*?,..)", options);

            public static string GetString(string input) {
                string value = "—";
                try {
                    value = $"{regex.Split(input)[2]}";
                }
                catch { }
                return value;
            }
        }

        public sealed class Requester {
            private static readonly Regex regex44 = new(@"<a href=""(?<space>.*?)epz/organization/view(?<space>.*?)>(?<val>.*?)<", options);
            private static readonly Regex regex223 = new(@"padding"">(?<space>.*?)<a href=""(?<space>.*?)epz/organization/view(?<space>.*?)>(?<val>.*?)</a>", options);

            public static string GetString(string input) {
                string value = "—";
                if (listProcurement[3] == "44-ФЗ") {
                    value = regex44.Split(input)[2];
                }
                else {
                    value = regex223.Split(input)[2];
                }
                return value;
            }
        }

        public sealed class PostAddress {
            private static readonly Regex regex = new(@"Почтовый адрес(?<space>.*?)>\n(?<space>.*?)\n *(?<val>.*?)\n", options);

            public static string GetString(string input) {
                string value = "—";
                try {
                    value = $"{regex.Split(input)[2]}";
                }
                catch { }
                return value;
            }
        }

        public sealed class Objects {
            private static readonly Regex regex44 = new(@"Объект закупки</span>\n(?<space>.*?)>(?<val>.*?)<", options);
            private static readonly Regex regex223 = new(@"Объект закупки</div>\n(?<space>.*?)\n *(?<val>.*?)\n", options);

            public static string GetString(string input) {
                string value = "—";
                if (listProcurement[3] == "44-ФЗ") {
                    value = regex44.Split(input)[2];
                }
                else {
                    value = regex223.Split(input)[2];
                }
                return value;
            }
        }

        public sealed class Location {
            private static readonly Regex regex = new(@"Место нахождения(?<space>.*?)>\n(?<space>.*?)\n *(?<val>.*?)\n", options);

            public static string GetString(string input) {
                string value = "—";
                try {
                    value = $"{regex.Split(input)[2]}";
                }
                catch { }
                return value;
            }
        }

        public sealed class Security {
            private static readonly Regex regex = new(@"Размер обеспечения исполнения контракта(?<space>.*?)>\n(?<space>.*?)>\n(?<space>.*?)\n(?<space>.*?)\n(?<space>.*?)\n *(?<val>.*?)\n", options);

            public static string GetString(string input) {
                string value = "—";
                try {
                    value = $"{regex.Split(input)[2]}";
                }
                catch { }
                value = value.Replace("&nbsp;", " ");
                return value;
            }
        }
        public sealed class SupplyAssurance
        {
            private static readonly Regex regex = new(@"Размер обеспечения заявки(?<space>.*?)>\n(?<space>.*?)>\n *(?<val>.*?)\n", options);

            public static string GetString(string input)
            {
                string value = "—";
                try
                {
                    value = $"{regex.Split(input)[2]}";
                }
                catch { }
                value = value.Replace("&nbsp;", " ");
                return value;
            }
        }

        public sealed class UTC {
            private static readonly Regex regex = new(@"\(UTC(?<val>.*?)\)", options);

            public static string GetString(string input) {
                string value = "—";
                try {
                    value = $"{regex.Split(input)[1]}";
                }
                catch { }
                return value;
            }
        }
        public sealed class ProvidingAguarantee
        {
            private static readonly Regex regex = new(@"Размер обеспечения гарантийных обязательств(?<space>.*?)>\n(?<space>.*?)>\n *(?<val>.*?)\n", options);

            public static string GetString(string input)
            {
                string value = "—";
                try
                {
                    value = $"{regex.Split(input)[2]}";
                }
                catch { }
                value = value.Replace("&nbsp;", " ");
                return value;
            }
        }
    }
}
