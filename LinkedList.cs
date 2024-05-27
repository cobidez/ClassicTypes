using System;

namespace ClassicTypes;

internal class LinkedList<T>
{
    private uint length;
    public uint Length 
    {
        get { return length; } 
    }

    private uint MaxAllowedLenghtForAdding
    {
        get { return length + 1; }
    }

    private uint MaxAllowedLenghtForRemoving
    {
        get { return length - 1; }
    }

    private LinkedListNode<T> firstNode;
    private LinkedListNode<T> lastNode;

    private const string listLengthErrorTemplate = "Can't {0} value with index = [{1}] cause length is {2}";

    public void Add(T value, uint index)
    {
        if (index > MaxAllowedLenghtForAdding)
        {
            PrintListLengthErrorMessage("add", index);
            return;
        }

        if (value == null)
        {
            Console.WriteLine("Adding null as value not allowed");
            return;
        }

        LinkedListNode<T> newNode = new LinkedListNode<T>(value);

        if (Length == 0)
        {
            firstNode = newNode;
            lastNode = newNode;
        }
        else if (index == 0)
        {
            firstNode.Previous = newNode;
            newNode.Next = firstNode;
            firstNode = newNode;
        }
        else if (index == MaxAllowedLenghtForAdding)
        {
            lastNode.Next = newNode;
            newNode.Previous = lastNode;
            lastNode = newNode;
        }
        else
        {
            var nodeForChange = GetNodeByIndex(index);

            // Добавляем новой ноде ссылки
            newNode.Previous = nodeForChange.Previous.Next;
            newNode.Next = nodeForChange;

            // Перевязываем старым нодам ссылки
            nodeForChange.Previous.Next = newNode;
            nodeForChange.Previous = newNode;            
        }

        length++;
    }

    public void Add(T value)
    {
        Add(value, MaxAllowedLenghtForAdding);
    }

    public void PrintValues()
    {
        if (firstNode != null)
        {
            for (LinkedListNode<T> i = firstNode; i != null; i = i.Next)
            {
                Console.WriteLine(i.Value.ToString());
            }
        }
    }

    private LinkedListNode<T> GetNodeByIndex(uint index)
    {
        uint middleIndex = length / 2;
        LinkedListNode<T> result;

        if (index <= middleIndex)
        {
            uint i = 0;
            result = firstNode;
            while (i < index)
            {
                result = result.Next;
                i++;
            }
        } 
        else
        {
            uint i = MaxAllowedLenghtForRemoving; // Переименовать переменную
            result = lastNode;
            while (i > index)
            {
                result = result.Previous;
                i--;
            }
        }      

        return result;
    }

    public T GetValueByIndex(uint index)
    {
        if (index >= Length)
        {
            PrintListLengthErrorMessage("get", index);
            return default;
        }

        return GetNodeByIndex(index).Value;
    }

    public void Remove(uint index)
    {
        if (index > MaxAllowedLenghtForRemoving)
        {
            PrintListLengthErrorMessage("remove", index);
            return;
        }

        var node = GetNodeByIndex(index);

        if (index == 0)
        {
            if (length == 1)
            {
                firstNode = null;
                lastNode = null;
            }
            else
            {
                firstNode = node.Next;
                firstNode.Previous = null;
            }
        }
        else if (index == MaxAllowedLenghtForRemoving)
        {
            lastNode = node.Previous;
            lastNode.Next = null;
        }
        else
        {
            var prevNode = node.Previous;
            prevNode.Next = node.Next;
            node.Next.Previous = prevNode;
        }

        node = null;
        length--;
    }

    private void PrintListLengthErrorMessage(string operationType, uint index)
    {
        Console.WriteLine(listLengthErrorTemplate, operationType, index, Length);
    }
}

public class LinkedListNode<T>
{
    public T Value { get; set; }

    public LinkedListNode<T> Previous { get; set; }
    public LinkedListNode<T> Next { get; set; }

    public LinkedListNode(T value)
    {
        Value = value;
    }
}

