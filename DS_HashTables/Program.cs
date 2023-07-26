namespace DS_HashTables
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var frequencyCounter = new MyMapNode<string, int>(10);
            frequencyCounter.FrequencyCounter("To be or not to be");

        }
    }

    public class MyMapNode<K, V>
    {
        public struct KeyValue<K, V>
        {
            public K Key { get; set; }
            public V Value { get; set; }
        }
        private readonly LinkedList<KeyValue<K, V>>[] items;

        public MyMapNode(int size)
        {
            items = new LinkedList<KeyValue<K, V>>[size];
        }

        protected int GetArrayPosition(K key)
        {
            int position = key.GetHashCode() % items.Length;
            return Math.Abs(position);
        }

        public V Get(K key)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
            foreach (var item in linkedList)
            {
                if (item.Key.Equals(key))
                {
                    return item.Value;
                }
            }
            return default(V);
        }

        protected LinkedList<KeyValue<K, V>> GetLinkedList(int position)
        {
            LinkedList<KeyValue<K, V>> linkedList = items[position];
            if (linkedList == null)
            {
                linkedList = new LinkedList<KeyValue<K, V>>();
                items[position] = linkedList;
            }
            return linkedList;
        }

        public void Add(K key, V value)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
            var item = new KeyValue<K, V>() { Key = key, Value = value };
            linkedList.AddLast(item);
        }
        public void Update(K key, V value)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
            bool itemFound = false;

            KeyValue<K, V> foundItem = default(KeyValue<K, V>);

            foreach (KeyValue<K, V> item in linkedList)
            {
                if (item.Key.Equals(key))
                {
                    foundItem = item;
                    itemFound = true;
                }
            }

            if (itemFound)
            {
                linkedList.Remove(foundItem);
                foundItem.Value = value;
                linkedList.AddLast(foundItem);
            }
            else
            {
                var item = new KeyValue<K, V>() { Key = key, Value = value };
                linkedList.AddLast(item);
            }
        }


        public void FrequencyCounter(string sentence)
        {
            string[] words = sentence.ToLower().Split(' ');
            MyMapNode<string, int> frequencyTable = new MyMapNode<string, int>(words.Length);
            foreach (var word in words)
            {
                int value = frequencyTable.Get(word);
                if (value == 0)
                {
                    frequencyTable.Add(word, 1);
                }
                else
                {
                    frequencyTable.Update(word, value + 1);
                }
            }

            HashSet<string> processedWords = new HashSet<string>();
            foreach (var word in words)
            {
                if (!processedWords.Contains(word))
                {
                    Console.WriteLine($"Frequency of word '{word}' is {frequencyTable.Get(word)}");
                    processedWords.Add(word);
                }
            }
        }


    }
}