using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Laba1_SPO_Hashtable
{
    class Program
    {
        static void Main(string[] args)
        {
            //Создание хеш-таблицы
            Hashtable hashtable = new Hashtable();

            //Список с элементами ключ - значение (метод цепочек)
            List<Item> fileElements = AddElements("Laba1.txt");

            //Упорядоченный список
            List<Item> filtredList = AddElementsList("Laba1.txt");

            //Добавление элементов в хеш-таблицу (метод цепочек)
            foreach (var item in fileElements)
            {
                hashtable.Add(item.Key, item.Value);
            }

            ShowHashtable(hashtable, "Created Hashtable");

            //Поиск элемента по упорядоченному списку
            Console.Write("Введите ключ элемента для поиска: ");
            string findObj = Console.ReadLine();

            if (string.IsNullOrEmpty(findObj))
            {
                throw new ArgumentNullException(nameof(findObj));
            }

            Item result = null;
            int count = 0;

            for (int i = 0; i < filtredList.Count; i++)
            {
                count += 1;

                if (filtredList[i].Key == findObj)
                {
                    result = filtredList[i];

                    Console.WriteLine($"Найденное слово: {result.Value}");
                    Console.WriteLine($"Количество итераций, затраченных на поиск элемента \nв упорядоченном списке: {count}");
                }

                if (result == null && count == filtredList.Count)
                {
                    Console.WriteLine("Таблица не содержит элемента с таким ключом.");
                }
            }

            Console.ReadKey();
        }


        //Метод вывода всех элементов хеш-таблицы на экран
        private static void ShowHashtable(Hashtable hashtable, string title)
        {
            //Проверка входных значений
            if (hashtable == null)
            {
                throw new ArgumentNullException(nameof(hashtable));
            }

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            //Вывод на экран всех элементов
            Console.WriteLine(title);
            foreach (var item in hashtable.Items)
            {
                Console.WriteLine(item.Key) ;

                foreach(var value in item.Value)
                {
                    Console.WriteLine($"\t{value.Value}: {value.Key}");
                }
            }
            Console.WriteLine();
        }


        //Добавление элементов из файла в список, чтобы потом уже работать с ним
        private static List<Item> AddElements(string fileName)
        {
            // list - список, в который выгружаются слова из файла
            // itemList - список с элементами (ключ - значение)

            List<string> list = File.ReadAllLines(fileName).ToList();
            List<Item> itemList = new List<Item>();

            //Добавление элементов Item в список itemList
            foreach (var item in list)
            {
                string iKey = item.GetHashCode().ToString();

                Item item1 = new Item(iKey, item);

                itemList.Add(item1);
            }

            return itemList;
        }

        private static List<Item> AddElementsList(string fileName)
        {
            List<string> list = File.ReadAllLines(fileName).ToList();
            list.Sort();

            List<Item> secondFilter = new List<Item>();

            foreach (var item in list)
            {
                string iKey = item.GetHashCode().ToString();
                Item item1 = new Item(iKey, item);

                secondFilter.Add(item1);
            }

            return secondFilter;
        }
    }
}
