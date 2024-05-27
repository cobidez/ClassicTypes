namespace ClassicTypes;

internal class Program
{
    static void Main(string[] args)
    {
        LinkedList<int> list = new LinkedList<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.Add(5);
        list.Add(6);
        list.Add(7);
        list.Add(8);

        list.Remove(6);

        Console.WriteLine("-------------");
        list.PrintValues();

        Console.WriteLine("-------------");
        Console.WriteLine(list.Length);
        Console.WriteLine(list.GetValueByIndex(6));
    }
}
