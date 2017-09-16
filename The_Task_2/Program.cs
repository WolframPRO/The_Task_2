using System;
using System.IO;

namespace The_Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string strTemp = File.ReadAllText("INPUT.TXT").Trim();
            
            if (strTemp.Length == 0) File.WriteAllText("OUTPUT.TXT", "0");
            else if (strTemp.Length == 1) File.WriteAllText("OUTPUT.TXT", "1");
            else
            {

                Lab[] c = new Lab[strTemp.Length + 1];
                c[0] = new Lab("1");
                c[1] = new Lab("1");


                for (int i = 2; i < c.Length; i++)
                {
                    int cur = Int32.Parse(strTemp[i - 2].ToString() + strTemp[i-1]);
                    if (cur > 9 && cur < 34) c[i] = c[i - 1] + c[i - 2];
                    else c[i] = c[i - 1];
                }


                File.WriteAllText("OUTPUT.TXT", c[c.Length - 1].Show());
            }
        }
    }

    
    class Lab
    {
        byte[] Number;           //Массив под длинное число.
        public int Length       //Св-во с длиной числа
        {
            get
            {
                return Number.Length;
            }
        }

        private Lab(int Len)   //Внутренний конструктор для оператора +
        {
            Number = new byte[Len];
            for (int i = 0; i < Len; i++)
            {
                Number[i] = 0;
            }
        }

        public Lab(string Num)  //Конструктор. Принимает на вход строку
            //можно заметить, что число заполняется с конца
            //Это удобней, поверьте.
        {
            Number = new byte[Num.Length];
            for (int i = Num.Length - 1, j = 0; i >= 0; i--, j++)
            {
                Number[j] = (byte)((int)Num[i] - 48);
            }
        }

        public string Show()     //Метод для отображения числа.
        {
            string str = "";
            for (int i = Number.Length - 1; i >= 0; i--)
            {
                if (i == (Number.Length - 1) && Number[i] == 0) continue;
                str += Number[i];
            }
            Console.WriteLine();
            return str;
        }
        //перегруженный оператор "+". Исключает ошибки.
        public static Lab operator +(Lab ob1, Lab ob2)
        {
            int Len = ob1.Length > ob2.Length ? ob1.Length : ob2.Length;
            Lab result = new Lab(Len + 1);
            byte buffer = 0;
            for (int i = 0; i < Len + 1; i++)
            {
                if ((i < ob1.Length) && (i < ob2.Length))
                {
                    result.Number[i] += ob1.Number[i];
                    result.Number[i] += ob2.Number[i];
                    result.Number[i] += buffer;
                    buffer = result.Number[i];
                    buffer /= 10;
                    result.Number[i] %= 10;
                }
                else
                {
                    if (ob1.Length > ob2.Length)
                    {
                        for (int j = i; j < Len; j++)
                        {
                            result.Number[j] = ob1.Number[j];
                            result.Number[j] += buffer;
                            buffer = result.Number[j];
                            buffer /= 10;
                            result.Number[j] %= 10;
                        }
                        result.Number[Len] = buffer;
                        break;
                    }
                    else
                    {
                        for (int j = i; j < Len; j++)
                        {
                            result.Number[j] = ob1.Number[j];
                            result.Number[j] += buffer;
                            buffer = result.Number[j];
                            buffer /= 10;
                            result.Number[j] %= 10;
                        }
                        result.Number[Len] = buffer;
                        break;
                    }
                }
            }
            return result;
        }

        //перегруженные операторы сравнения.
        public static bool operator ==(Lab ob1, Lab ob2)
        {
            if (ob1.Length != ob2.Length) return false;
            for (int i = 0; i < ob1.Length; i++)
                if (ob1.Number[i] != ob2.Number[i]) return false;
            return true;
        }
        public static bool operator !=(Lab ob1, Lab ob2)
        {
            if (ob1.Length != ob2.Length) return true;
            for (int i = 0; i < ob1.Length; i++)
                if (ob1.Number[i] != ob2.Number[i]) return true;
            return false;
        }
    }

}
