namespace DS_HashTables
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string phrase = "Paranoids are not paranoid because they are paranoid but because they keep putting themselves deliberately into paranoid avoidable situations";
            MyMapNode<string, int> hash = new MyMapNode<string, int>(10);
            hash.FrequencyCounter(phrase);
            Console.ReadLine();
        }
    }
}

public class MyMapNode<K, V>
{
    private readonly int size;
    private readonly LinkedList<KeyValue<K, V>>[] items;

    public MyMapNode(int size)
    {
        this.size = size;
        this.items = new LinkedList<KeyValue<K, V>>[size];
    }

    protected int GetArrayPosition(K key)
    {
        int position = key.GetHashCode() % size;
        return Math.Abs(position);
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

    public V Get(K key)
    {
        int position = GetArrayPosition(key);
        LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
        foreach (KeyValue<K, V> item in linkedList)
        {
            if (item.Key.Equals(key))
            {
                return item.Value;
            }
        }

        return default(V);
    }

    public void Add(K key, V value)
    {
        int position = GetArrayPosition(key);
        LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
        KeyValue<K, V> item = new KeyValue<K, V>() { Key = key, Value = value };
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
            var newItem = new KeyValue<K, V>() { Key = key, Value = value };
            linkedList.AddLast(newItem);
        }
    }


    public void FrequencyCounter(string phrase)
    {
        MyMapNode<string, int> hash = new MyMapNode<string, int>(10);
        string[] words = phrase.ToLower().Split(' ');

        foreach (string word in words)
        {
            int value = hash.Get(word);

            if (value == 0)
            {
                hash.Add(word, 1);
            }
            else
            {
                hash.Update(word, value + 1);
            }
        }

        HashSet<string> processedWords = new HashSet<string>();

        foreach (string word in words)
        {
            if (!processedWords.Contains(word))
            {
                Console.WriteLine("Frequency of " + word + " is " + hash.Get(word));
                processedWords.Add(word);
            }
        }
    }

    /*public void FrequencyCounter(string phrase)
    {
        MyMapNode<string, int> hash = new MyMapNode<string, int>(10);
        string[] words = phrase.ToLower().Split(' ');

        foreach (string word in words)
        {
            int value = hash.Get(word);

            if (value == 0)
            {
                hash.Add(word, 1);
            }
            else
            {
                hash.Add(word, value + 1);
            }
        }

        foreach (string word in words)
        {
            Console.WriteLine("Frequency of " + word + " is " + hash.Get(word));
        }
    }
*/

}

public struct KeyValue<K, V>
{
    public K Key { get; set; }
    public V Value { get; set; }
}

/*        public void FrequencyCounter(string sentence)
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
*/