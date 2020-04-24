using System;

namespace Laba1_SPO_Hashtable
{
    class Item
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public Item(string key, string value)
        {
            //Проверка входных данных
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            //Устанавливаем значения
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return Key;
        }
    }
}
