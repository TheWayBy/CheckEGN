using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EGN
{
    public class Program
    {
        public static void Main()
        {
            // get egn
            string egn = GetEGN();

            try
            {
                // check if only numbers are entered
                CheckIfContainsOnlyTenNumbers(egn);

                //check birth year - the numbers from 00 to 99 are valid, so no check needed => check months
                // check months 
                int months = CheckMonths(int.Parse(egn[2].ToString()), int.Parse(egn[3].ToString()));

                // get exact birth year
                int year = GetBirthYear(int.Parse(egn[0].ToString()), int.Parse(egn[1].ToString()), int.Parse(egn[2].ToString()), int.Parse(egn[3].ToString()));

                //check days
                CheckBirthDays(int.Parse(egn[4].ToString()), int.Parse(egn[5].ToString()), months, year);

                // check baby number 
                CheckBabyNumber(int.Parse(egn[6].ToString()), int.Parse(egn[7].ToString()));

                // check sex
                CheckSex(int.Parse(egn[8].ToString()));

                // check last symbol
                CheckLastDigit(egn);

                Console.WriteLine("EGN is OK!");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public static string GetEGN()
        {
            Console.WriteLine("Please enter your EGN: ");
            string inputEGN = Console.ReadLine();
            Console.WriteLine("You entered: {0}", inputEGN);

            return inputEGN;
        }

        public static void CheckIfContainsOnlyTenNumbers(string inputEGN)
        {
            Regex regex = new Regex(@"^[0-9]+${10}");
            if (!regex.IsMatch(inputEGN))
            {
                throw new Exception("You entered bad characters! Please enter only numbers! ");
            }
        }

        public static int CheckMonths(int firstDigit, int secondDigit)
        {
            int tmpMonths = firstDigit * 10 + secondDigit;

            // [1900 - 1999]
            if (tmpMonths >= 1 && tmpMonths <= 12)
            {
                return tmpMonths;
            }
            // [1800 - 1899]
            else if (tmpMonths >= 21 && tmpMonths <= 32)
            {
                tmpMonths -= 20;
            }
            // [2000 - 2099]
            else if (tmpMonths >= 41 && tmpMonths <= 52)
            {
                tmpMonths -= 40;
            }
            else
            {
                throw new Exception("You entered wrong months!");
            }

            return tmpMonths;
        }

        public static int GetBirthYear(int firstYearDigit, int secondYearDigit, int firstMonthDigit, int secondMonthDigit)
        {
            int tmpyear, tmpMonth;

            tmpyear = firstYearDigit * 10 + secondYearDigit;
            tmpMonth = firstMonthDigit * 10 + secondMonthDigit;

            // [1900 - 1999]
            if (tmpMonth >= 1 && tmpMonth <= 12)
            {
                tmpyear += 1900;
            }
            // [1800 - 1899]
            else if (tmpMonth >= 21 && tmpMonth <= 32)
            {
                tmpyear += 1800;
            }
            // [2000 - 2099]
            else if (tmpMonth >= 41 && tmpMonth <= 52)
            {
                tmpyear += 2000;
            }

            return tmpyear;
        }

        public static void CheckBirthDays(int firstDayDigit, int secontDayDigit, int birthMonth, int birthYear)
        {
            int tmpDay = firstDayDigit * 10 + secontDayDigit;
            string tmpdate = birthMonth + "/" + tmpDay + "/" + birthYear;
            DateTime temp;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTimeStyles styles = DateTimeStyles.None;

            if (!(DateTime.TryParse(tmpdate, culture, styles, out temp)))
            {
                throw new Exception("The date is NOK");
            }
        }

        public static void CheckBabyNumber(int firstNumb, int secondNumb)
        {
            if (firstNumb == 0 && secondNumb == 0)
            {
                throw new Exception("You entered wrong baby number!");
            }
        }

        public static void CheckSex(int sex)
        {
            string inputSex;
            string egnSex;

            Console.WriteLine("Please enter human's sex: (male/female)");
            while (true)
            {
                inputSex = Console.ReadLine();
                inputSex = inputSex.Trim();
                if (inputSex.Equals("male") || inputSex.Equals("female"))
                {
                    break;
                }
                Console.WriteLine("Wrong sex, please enter again: ");
            }

            if (sex % 2 == 0)
            {
                egnSex = "male";
            }
            else
            {
                egnSex = "female";
            }

            if (!inputSex.Equals(egnSex))
            {
                throw new Exception("EGN sex is wrong!");
            }
        }

        public static void CheckLastDigit(string str)
        {
            int lastDigit = int.Parse(str[9].ToString());
            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                int numb = int.Parse(str[i].ToString());

                switch (i)
                {
                    case 0:
                        sum += numb * 2;
                        break;
                    case 1:
                        sum += numb * 4;
                        break;
                    case 2:
                        sum += numb * 8;
                        break;
                    case 3:
                        sum += numb * 5;
                        break;
                    case 4:
                        sum += numb * 10;
                        break;
                    case 5:
                        sum += numb * 9;
                        break;
                    case 6:
                        sum += numb * 7;
                        break;
                    case 7:
                        sum += numb * 3;
                        break;
                    case 8:
                        sum += numb * 6;
                        break;
                    default:
                        break;
                }
            }

            double tmpOstatuk = sum / 11;
            int ostatuk = (int)(Math.Floor(tmpOstatuk));
            int otg = sum - ostatuk * 11;

            if (otg != lastDigit)
            {
                throw new Exception("Wrong EGN entered!");
            }
        }
    }
}
