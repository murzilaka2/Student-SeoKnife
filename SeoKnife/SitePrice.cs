using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeoKnife
{
    public class SitePrice
    {
        //Стоимость сайта
        public ArrayList Subjects = new ArrayList();
        private int FinalPrice { get; set; }

        public SitePrice()
        {
            SitesSubjects();
        }
        //Оценка стоимости сайта
        public int SiteCost(int Tiz, string Subject, int Visitors, int Watches, bool GoogleIndex, bool YandexIndex)
        {
            FinalPrice += SiteTizCost(Tiz, Visitors);
            FinalPrice += SiteVisitors(Visitors);
            FinalPrice = SiteWatches(Visitors, Watches);
            FinalPrice = SiteIndex(GoogleIndex, YandexIndex);
            return FinalPrice;
        }
        //Темы сайтов
        private void SitesSubjects()
        {
            Subjects.Add("Дизайн");
            Subjects.Add("Авто");//38.49
            Subjects.Add("Бизнес");//44.76
            Subjects.Add("Бытовая и Электронная техника");//21.5
            Subjects.Add("Животные");//13.24
            Subjects.Add("Закон и Власть");//7.77
            Subjects.Add("Игры");//8.15
            Subjects.Add("Интернет");//27.23
            Subjects.Add("Каталог сайтов");//20.76
            Subjects.Add("Кино и Музыка");//5
            Subjects.Add("Компьютеры");//11.93
            Subjects.Add("Культура");//13.68
            Subjects.Add("Мебель");//43.53
            Subjects.Add("Медицина");//21.58
            Subjects.Add("Мода и Красота");//11.35 &&&
            Subjects.Add("Недвижимость");//50.37
            Subjects.Add("Обучение");//47.24
            Subjects.Add("Отдых и Еда");//28.41
            Subjects.Add("Политика");//13.63
            Subjects.Add("Природа");//20.87
            Subjects.Add("Производство");//34.22
            Subjects.Add("Психология");//24.73
            Subjects.Add("Работа");//16.84
            Subjects.Add("Региональные порталы");//17.41
            Subjects.Add("Реклама");//45.38
            Subjects.Add("Религия");//11.76
            Subjects.Add("СМИ");//10.05
            Subjects.Add("Связь");//24.28
            Subjects.Add("Семья");//16.10
            Subjects.Add("Социальные сети");//15.23
            Subjects.Add("Спорт");//16.75
            Subjects.Add("Справки");//54.73
            Subjects.Add("Стройка и Ремонт");//62.45
            Subjects.Add("Торговля");//42.41
            Subjects.Add("Туризм");//22.27
            Subjects.Add("Финансы");//45.71
            Subjects.Add("Хобби");//14.16
            Subjects.Add("Энциклопедии");//9.17
            Subjects.Add("Юмор");//3.86
        }
        //Стоимость тиц
        public int SiteTizCost(int Tiz, int Visitors)
        {
            int TizPrice = 0;
            if (Tiz >= 10 && Tiz <= 30)
            {
                if (Visitors >= 4000)
                {
                    TizPrice = (150 * 15) * 5;
                }
                else { TizPrice = ((150 * 15) * 5) / 2; }
            }
            if (Tiz >= 30 && Tiz <= 50)
            {
                if (Visitors >= 4000)
                {
                    TizPrice = (250 * 15) * 5;
                }
                else { TizPrice = ((250 * 15) * 5) / 2; }
            }
            else if (Tiz >= 60 && Tiz <= 100)
            {
                if (Visitors >= 4000)
                {
                    TizPrice = (450 * 15) * 5;
                }
                else { TizPrice = ((450 * 15) * 5) / 2; }
            }
            else if (Tiz >= 110 && Tiz <= 300)
            {
                if (Visitors >= 4000)
                {
                    TizPrice = (600 * 15) * 5;
                }
                else { TizPrice = ((600 * 15) * 5) / 2; }
            }
            else if (Tiz >= 310 && Tiz <= 600)
            {
                if (Visitors >= 3500)
                {
                    TizPrice = (800 * 15) * 5;
                }
                else { TizPrice = ((800 * 15) * 5) / 2; }
            }
            else if (Tiz >= 610 && Tiz <= 1500)
            {
                if (Visitors >= 3000)
                {
                    TizPrice = (950 * 15) * 5;
                }
                else { TizPrice = ((950 * 15) * 5) / 2; }
            }
            else if (Tiz > 1500)
            {
                if (Visitors >= 3000)
                {
                    TizPrice = (2000 * 15) * 5;
                }
                else { TizPrice = ((2000 * 11) * 5); }
            }
            return TizPrice;
        }
        //Посещаемость сайта для рекламы
        private int SiteVisitors(int Visitors)
        {
            //Цена в $ за количество посетителей. Это минимально приблизительная оценка. В ней расчитывается стандартный доход по всем сайтам. Минимальный и максимальный доход
            //зависит от многих других характеристик сайтов и цены рекломадателей. Из данной оценки стоит ориентироваться на минимальную сумму дохода для сайта, при самой банальной 
            //планировки рекламы и средне заинтересованных посетителей в рекламе, к примеру с расчетом  минимум : 3000 посетителей / на 25-30 кликов, максимум :  3000 посетителей / на 80-300 кликов.
            //с приближенной к тематике ценной за клик - 0,09$.
            //Для каждой тематики идет соответствующая цена за клик по текущей статистики рекламодателей.
            int VisitorsPriceMin = 0;
            int VisitorsPriceMax = 0;

            if (Visitors >= 1500 && Visitors < 3000)
            {
                VisitorsPriceMin = 2 * 56;
                VisitorsPriceMax = 4 * 56;
            }
            else if (Visitors >= 3000 && Visitors <= 5000)
            {
                VisitorsPriceMin = 5;
                VisitorsPriceMax = 7;
            }
            else if (Visitors >= 5000 && Visitors < 10000)
            {
                VisitorsPriceMin = 8;
                VisitorsPriceMax = 15;
            }
            else if (Visitors >= 10000 && Visitors < 15000)
            {
                VisitorsPriceMin = 17;
                VisitorsPriceMax = 42;
            }
            else if (Visitors >= 15000 && Visitors < 20000)
            {
                VisitorsPriceMin = 44;
                VisitorsPriceMax = 62;
            }
            else if (Visitors >= 20000 && Visitors < 25000)
            {
                VisitorsPriceMin = 64;
                VisitorsPriceMax = 84;
            }
            else if (Visitors >= 25000 && Visitors < 30000)
            {
                VisitorsPriceMin = 86;
                VisitorsPriceMax = 114;
            }
            else if (Visitors >= 30000 && Visitors < 40000)
            {
                VisitorsPriceMin = 134;
                VisitorsPriceMax = 200;

            }
            else if (Visitors >= 40000 && Visitors < 50000)
            {
                VisitorsPriceMin = 210;
                VisitorsPriceMax = 290;

            }
            else if (Visitors >= 50000 && Visitors < 60000)
            {
                VisitorsPriceMin = 310;
                VisitorsPriceMax = 400;

            }
            else if (Visitors >= 60000 && Visitors < 70000)
            {
                VisitorsPriceMin = 420;
                VisitorsPriceMax = 510;
            }
            else if (Visitors >= 70000 && Visitors < 80000)
            {
                VisitorsPriceMin = 530;
                VisitorsPriceMax = 620;
            }
            else if (Visitors >= 80000 && Visitors < 90000)
            {
                VisitorsPriceMin = 640;
                VisitorsPriceMax = 730;
            }
            else if (Visitors >= 90000 && Visitors < 100000)
            {
                VisitorsPriceMin = 750;
                VisitorsPriceMax = 840;
            }
            else if (Visitors >= 100000 && Visitors < 110000)
            {
                VisitorsPriceMin = 850;
                VisitorsPriceMax = 950;
            }
            else if (Visitors >= 110000 && Visitors < 120000)
            {
                VisitorsPriceMin = 960;
                VisitorsPriceMax = 1020;
            }

            int sum = (VisitorsPriceMin + VisitorsPriceMax) / 2;
            sum = sum * 56;
            sum = sum * 5;
            return sum;
        }
        //Увеличение цены в зависимости от просмотров
        private int SiteWatches(double Visitors, double Watches)
        {
            double result = Watches / Visitors;
            if (result < 1.5)
            {
                result = (FinalPrice * 20) / 100;
                return FinalPrice = FinalPrice - Convert.ToInt32(result);
            }
            else if (result >= 1.5 && result < 3)
            {
                result = (FinalPrice * 20) / 100;
                return FinalPrice = FinalPrice + Convert.ToInt32(result);
            }
            else if (result >= 3 && result < 4)
            {
                result = (FinalPrice * 25) / 100;
                return FinalPrice = FinalPrice + Convert.ToInt32(result);
            }
            else if (result >= 4 && result < 5)
            {
                result = (FinalPrice * 35) / 100;
                return FinalPrice = FinalPrice + Convert.ToInt32(result);
            }
            return 0;
        }
        //Индексация сайта
        private int SiteIndex(bool GoogleIndex, bool YandexIndex)
        {
            int result = 0;
            if (GoogleIndex == false)
            {
                result = (FinalPrice * 10) / 100;
            }
            else if (YandexIndex == false)
            {
                result += (FinalPrice * 10) / 100;
            }
            return FinalPrice - result;
        }
    }
}