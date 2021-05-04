using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data
{

    class date
    {
        public static int day { get; set; }
        public static int mounth { get; set; }
        public static int years { get; set; }

        public static void dated()
        {
            DateTime thisDay = DateTime.Today;
            day = Convert.ToInt32(thisDay.ToString("dd"));
            mounth = Convert.ToInt32(thisDay.ToString("MM"));
            years = Convert.ToInt32(thisDay.ToString("yyyy"));
            Calendarec.LeapYear();
        }

        public static void leftArrow()
        {
            /*Console.WriteLine("leftArrow");*/
            if (mounth <= 1)
            {
                years--;
                mounth = 12;
                
            }
            else
            {
                mounth--;
            }
        }

        public static void rightArrow()
        {
            if (mounth >= 12)
            {
                years++;
                mounth = 1;
            }
            else
            {
                mounth++;
            }
        }
    }

    public class Date
    {
        public string Day { get; set; }
        public string Mounth { get; set; }
        public string Year { get; set; }
        private string DayOfWeek { get; set; }
        private string FullDate { get; set; } //Использовать в БД для указания даты заметки

        public Date(string day, string mounth, string year, string dayOfWeek)
        {
            Day = day;
            Mounth = mounth;
            Year = year;
            DayOfWeek = dayOfWeek;
            FullDate = day + mounth + year;
        }
    }

    public class Calendarec
    {
        static string[] DayMounths = {"31", "28", "31", "30", "31", "30", "31", "31", "30", "31", "30", "31"};
        static string[] Mounths = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"};
        static string[] WeekDays = { "понедельник", "вторник", "среда", "четверг", "пятница", "суббота", "воскресенье" }; 
        public static int test1 = 0;
        public static Date[,] Date = new Date[6, 7];
        public static List<Date> list = new List<Date>();

        public static void Refresh()
        {
            /*if (lr)
            {
                date.leftArrow();
            }
            else date.rightArrow();*/
            clear();
            Calendare();
            Conver();
        }

        public static void clear()
        {
            Console.WriteLine("Refresh, list count:{0}", list.Count);
            list.Clear();
        }

        public static void LeapYear(int years = 0)
        {
            if (years == 0)
            {
                if (DateTime.IsLeapYear(date.years))
                {
                    DayMounths[1] = "29";
                }
                else
                {
                    DayMounths[1] = "28";
                }
            }
            else
            {
                if (DateTime.IsLeapYear(years))
                {
                    DayMounths[1] = "29";
                }
                else
                {
                    DayMounths[1] = "28";
                }
            }

        }

        private static int vfor()
        {
            if (date.mounth == 1)
            {
                return 11;
            }

            else return date.mounth - 1;
        }

        private static string DefineDayOfWeek(int a, int b, int c)
        {
            Console.WriteLine("{0}   {1}   {2}", a, b, c);
            DateTime datels = new DateTime(a, b , c);
            return datels.ToString("dddd");
        }

        public static void Calendare()
        {
            
            DateTime date1 = new DateTime(date.years, date.mounth, 1);
            string DayWeek = date1.ToString("dddd");
            int dat = 0;
            bool point = false;
            int virtualYear = 0;
            for(int i = 0; i < 7; i++)
            {
                if(DayWeek == WeekDays[i])
                {
                    dat = i;
                    break;
                }
            }
            /*Console.WriteLine(dat);*/
            int posl = 1;
            int alt = 0;
            int zp = 0;
            for (int j = 0; j < 6; j++)
            {
                for(int z = 0; z < 7; z++)
                {
                    virtualYear = date.years;
                    if (dat != 0 && !point)
                    {
                        for (int a = 0; a < dat; a++)
                        {
                            virtualYear = date.years;
                            if (date.mounth - 2 == 0)
                            {
                                virtualYear--;
                            }
                            int l1 = Convert.ToInt32(DayMounths[vfor()]) - dat + a;
                            string z1 = Convert.ToString(l1);
                            LeapYear(virtualYear);
                            Date[0, a] = new Date(z1,
                                Convert.ToString(date.mounth - 1), Convert.ToString(virtualYear),
                                DefineDayOfWeek(virtualYear, vfor(), Convert.ToInt32(DayMounths[vfor()]) - dat + a));
                        }
                        z = dat;
                        point = true;
                    }
                    
                    if (Convert.ToInt32(DayMounths[date.mounth - 1]) >= j * 7 + z - dat + 1)
                    {
                        Console.WriteLine(date.mounth-1);
                        if((date.mounth - 1) == 0)
                        {
                            virtualYear--;
                            alt = 12;
                        }
                        else
                        {
                            alt = date.mounth - 1;
                        }
                        LeapYear(virtualYear);
                        Console.WriteLine("j: {0}   z: {1}  dat: {2}", j, z, dat);

                        Date[j, z] = new Date(Convert.ToString(j * 7 + z - dat + 1), Convert.ToString(date.mounth), Convert.ToString(virtualYear), DefineDayOfWeek(virtualYear, date.mounth, j * 7 + z - dat + 1));
                        continue;
                    }
                    if(date.mounth + 1 > 12)
                    {
                        zp = 1;
                        virtualYear++;
                    }
                    else
                    {
                        zp = date.mounth + 1;
                    }
                    LeapYear(virtualYear);
                    Console.WriteLine("posl: {0}    Mounth:{1}  year:{2}",posl, date.mounth, virtualYear);
                    Date[j, z] = new Date(Convert.ToString(posl), Convert.ToString(zp), Convert.ToString(virtualYear), DefineDayOfWeek(virtualYear, zp, posl));
                    posl++;
                }
            }
            Conver();
        }

        public static string Zapolnisc(int a, int b)
        {
            return Date[a, b].Day;
        }

        public static void Conver()
        {
            Console.WriteLine("Conver");
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    list.Add(Date[i, j]);
                    Console.WriteLine(Date[i,j]);
                }
            }
            Console.WriteLine(list.Count);
        }

        public static string vmounth()
        {
            return Mounths[date.mounth-1] + " " + Convert.ToString(date.years);
        }
    }
}
