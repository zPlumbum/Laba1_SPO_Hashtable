using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba1_SPO_Hashtable
{
    class Hashtable
    {
        private Dictionary<int, List<Item>> _items = null;

        public IReadOnlyCollection<KeyValuePair<int, List<Item>>> Items => _items?.ToList()?.AsReadOnly();


        public Hashtable()
        {
            _items = new Dictionary<int, List<Item>>(50);
        }

        //Добавление элемента в хеш-таблицу
        public void Add(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            //Создаем новый элемент таблицы
            Item item = new Item(key, value);

            //Получаем хеш-код его ключа
            var hash = GetHash(item.Key);

            List<Item> hashTableItem = null;

            if (_items.ContainsKey(hash))
            {
                hashTableItem = _items[hash];

                var oldElementWithKey = hashTableItem.SingleOrDefault(i => i.Key == item.Key);
                if (oldElementWithKey != null)
                {
                    throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}. Ключ элемента должен быть уникален", nameof(key));
                }

                _items[hash].Add(item);
            }
            else
            {
                hashTableItem = new List<Item> { item };
                _items.Add(hash, hashTableItem);
            }
        }


        //Поиск предмета по хеш-таблице
        public string Search(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var hash = GetHash(key);

            if (!_items.ContainsKey(hash))
            {
                return null;
            }

            var hashTableItem = _items[hash];

            if (hashTableItem != null)
            {
                Item item = hashTableItem.SingleOrDefault(i => i.Key == key);

                if (item != null)
                {
                    return item.Value;
                }
            }

            //Если ничего не найдено, возвращаем null
            return null;
        }


        //Удаление элемента из хеш-таблицы
        public void Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var hash = GetHash(key);

            if (!_items.ContainsKey(hash))
            {
                return;
            }

            var hashTableItem = _items[hash];

            Item item = hashTableItem.SingleOrDefault(i => i.Key == key);

            if (item != null)
            {
                hashTableItem.Remove(item);
            }            
        }


        //Получение хеш-кода ключа
        private int GetHash(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            var hash = value.Length;
            return hash;
        }
    }
}
