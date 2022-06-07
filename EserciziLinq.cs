using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MvcMovie
{
    static public class EserciziLinq
    {
        public static IEnumerable<int> Power(int number, int exponent)
        {
            int result = 1;

            for(int i = 0; i < exponent; i++)
            {
                //result = result * number;
                result = i;
                yield return result;
            }
        }
        public static void Esercizio1_yieldresult()
        {
            IEnumerable<int> iePowerMethod = Power(2,8);
            // Display powers of 2 up to the exponent of 8:
            foreach (int i in iePowerMethod)
            {
                Console.Write("{0} ", i);
            }
        }

        public static void Esercizio2_linq0()
        {
            // The Three Parts of a LINQ Query:
            // 1. Data source.
            int[] numbers = new int[11] { 0, 1, 2, 3, 4, 5, 6, 87, 63, 90, 97 };

            // 2. Query creation.
            // numQuery is an IEnumerable<int>
            IEnumerable<int> numQuery =
                from num in numbers
                where (((num % 2) == 0) && (num > 4))
                select num*1000;

            // 3. Query execution.
            foreach (int num in numQuery)
            {
                Console.Write("{0,1} ", num);
            }
            Console.WriteLine("\n");
            /*
             * MOLTO IMPORTANTE!!!!!
            In LINQ l'esecuzione della query è distinta dalla query stessa. 
            In altre parole, non sono stati recuperati dati solo creando una 
            variabile di query. Un modo per vederlo è l'esercizio che segue. 
            Se cambio numbers ma non tocco numQuery automaticamente l'esecuzione
            cambia.
           */
            numbers[0] = 32;
            foreach (int num in numQuery)
            {
                Console.Write("{0,1} ", num);
            }
            Console.Write("\n");
        }

        //-----------------------------//
        //Nuovo Esercizio Studenti

        enum GradeLevel
        {
            FirstYear = 1,
            SecondYear,
            ThirdYear,
            FourthYear
        };

        class Student
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int ID { get; set; }
            public GradeLevel? Year { get; set; }
            public List<int> ExamScores { get; set; }

            public Student()
            {
                return;
            }

            public Student(string FirstName, string LastName, int ID, GradeLevel Year, List<int> ExamScores)
            {
                this.FirstName = FirstName;
                this.LastName = LastName;
                this.ID = ID;
                this.Year = Year;
                this.ExamScores = ExamScores;
            }

            public Student(string FirstName, string LastName, int StudentID, List<int>? ExamScores = null)
            {
                this.FirstName = FirstName;
                this.LastName = LastName;
                ID = StudentID;
                this.ExamScores = ExamScores ?? Enumerable.Empty<int>().ToList();
            }

            public List<Student> CreaClasseStudenti()
            {
                List<Student> students = new()
                {
                    new(
                    FirstName: "Terry", LastName: "Adams", ID: 120,
                    Year: GradeLevel.SecondYear,
                    ExamScores: new() { 99, 82, 81, 79 }
                    ),
                    new(
                        "Fadi", "Fakhouri", 116,
                        GradeLevel.ThirdYear,
                        new() { 99, 86, 90, 94 }
                    ),
                    new(
                        "Hanying", "Feng", 117,
                        GradeLevel.FirstYear,
                        new() { 93, 92, 80, 87 }
                    ),
                    new(
                        "Cesar", "Garcia", 114,
                        GradeLevel.FourthYear,
                        new() { 97, 89, 85, 82 }
                    ),
                    new(
                        "Debra", "Garcia", 115,
                        GradeLevel.ThirdYear,
                        new() { 35, 72, 91, 70 }
                    ),
                    new(
                        "Hugo", "Garcia", 118,
                        GradeLevel.SecondYear,
                        new() { 92, 90, 83, 78 }
                    ),
                    new(
                        "Sven", "Mortensen", 113,
                        GradeLevel.FirstYear,
                        new() { 88, 94, 65, 91 }
                    ),
                    new(
                        "Claire", "O'Donnell", 112,
                        GradeLevel.FourthYear,
                        new() { 75, 84, 91, 39 }
                    ),
                    new(
                        "Svetlana", "Omelchenko", 111,
                        GradeLevel.SecondYear,
                        new() { 97, 92, 81, 60 }
                    ),
                    new(
                        "Lance", "Tucker", 119,
                        GradeLevel.ThirdYear,
                        new() { 68, 79, 88, 92 }
                    ),
                    new(
                        "Michael", "Tucker", 122,
                        GradeLevel.FirstYear,
                        new() { 94, 92, 91, 91 }
                    ),
                    new(
                        "Eugene", "Zabokritski", 121,
                        GradeLevel.FourthYear,
                        new() { 96, 85, 91, 60 }
                    )
                };
                return students;
            }
        }

        public static void Esercizio3()
        {
            Student GestoreClasse = new Student();
            List<Student> MiaClasse = GestoreClasse.CreaClasseStudenti();
            var highScores1 =
            from student in MiaClasse
            where student.ExamScores[0] > 90
            select new
            {
                Name = student.FirstName,
                Score = student.ExamScores[0]
            };

            foreach (var item in highScores1)
            {
                Console.WriteLine($"{item.Name, -15}{item.Score}");
            }

            Console.WriteLine("\n");

            var highScores2 =
                from student in MiaClasse
                where student.ExamScores.Max() > 95
                select new
                {
                    Name = student.FirstName,
                    Score = student.ExamScores.Max()
                };

            foreach (var item in highScores2)
            {
                Console.WriteLine($"{item.Name,-15}{item.Score}");
            }

            Console.WriteLine("\n");

            var highScores3 =
                from student in MiaClasse
                where student.Year == GradeLevel.FirstYear
                select new
                {
                    Name = student.FirstName,
                    Year = student.Year
                };

            foreach (var item in highScores3)
            {
                Console.WriteLine($"{item.Name, - 15}{item.Year}");
            }
            Console.WriteLine("\n");
        }

        public static void Esercizio4()
        {
            var filename = "ordini.xml";
            string sDirectoryCorrente = Directory.GetCurrentDirectory();
            var purchaseOrderFilepath = Path.Combine(sDirectoryCorrente, filename);

            XElement purchaseOrder = XElement.Load(purchaseOrderFilepath);

            IEnumerable<XElement> pricesByPartNos = 
                from item in purchaseOrder.Descendants("Item")
                where (int) item.Element("Quantity") * (decimal)item.Element("USPrice") > 100
                orderby (string) item.Element("PartNumer")
                select item;
        
            foreach (XElement item in pricesByPartNos)
            {
                Console.WriteLine($"{(string) item.Element("ProductName")}");
            }
            Console.WriteLine("\n");
        }

        public static void Esercizio5()
        {
            string pathA = @"C:\";
            //string pathB = @"C:\Prova2";

            System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(pathA);
            //System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(pathB);

            IEnumerable<System.IO.FileInfo> list1 = dir1.GetFiles();

            var highScores4 =
                from file in list1
                where file.Length > 1000
                select new
                {
                    Name = file.Name,
                    LADate = file.LastAccessTime,
                };

            foreach(var item in highScores4)
            {
                Console.WriteLine($"{ (string) item.Name, - 15} { item.LADate.ToString() }");
            }
            Console.WriteLine("\n");
        }



    }
}
