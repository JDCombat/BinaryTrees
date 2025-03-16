namespace BinaryTree;

public class Node<T>
{
    public T Data;
    public Node<T>? Left, Right, Parent;
    public int Count;

    public Node(T data)
    {
        this.Data = data;
        this.Count = 1;
        this.Parent = null;
        this.Left = null;
        this.Right = null;
    }
}

class BinarySearchTree<T> where T : IComparable<T>
{
    public Node<T>? Root;

    public BinarySearchTree()
    {
        Root = null;
    }

    public Node<T>? Search(Node<T>? current, T data)
    {
        if (current == null)
        {
            return null;
        }

        if (current.Data.CompareTo(data) == 0)
        {
            return current;
        }

        if (current.Data.CompareTo(data) > 0)
        {
            return Search(current.Left, data);
        }
        
        return Search(current.Right, data);
    }

    public void Insert(T data)
    {
        Node<T>? duplicated = Search(Root, data);
        if (duplicated != null)
        {
            duplicated.Count++;
            return;
        }
        Node<T> newNode = new Node<T>(data);
        if (Root == null)
        {
            Root = newNode;
            return;
        }
        Node<T>? currentNode = Root;
        while (currentNode != null)
        {

            if (currentNode.Data.CompareTo(data) > 0)
            {
                if (currentNode.Left == null)
                {
                    newNode.Parent = currentNode;
                    currentNode.Left = newNode;
                    currentNode = null;
                }
                else
                {
                    currentNode = currentNode.Left;
                }
            }
            else
            {
                if (currentNode.Right == null)
                {
                    newNode.Parent = currentNode;
                    currentNode.Right = newNode;
                    currentNode = null;
                }
                else
                {
                    currentNode = currentNode.Right;
                }
            }
        }
    }

    public static Node<T>? Predecessor(Node<T> node) {
        if (node.Left != null) {
            return FindMax(node.Left);
        }

        Node<T> currentNode = node;
        Node<T>? currentParent = node.Parent;

        while (currentParent != null) {
            if (currentNode == currentParent.Right) break;
            currentNode = currentParent;
            currentParent = currentNode.Parent;
        }
        return currentParent;
    }

    public static Node<T>? Successor(Node<T> node)
    {
        if (node.Right != null)
        {
            return FindMin(node.Right);
        }
        Node<T> currentNode = node;
        Node<T>? currentParent = node.Parent;

        while (currentParent != null)
        {
            if (currentNode == currentParent.Left) break;
            currentNode = currentParent;
            currentParent = currentNode.Parent;
        }
        return currentParent;
    }

    public static Node<T>? FindMax(Node<T>? node) {
        if (node == null) return null;
        while (node.Right != null) {
            node = node.Right;
        }
        return node;
    }

    public static Node<T>? FindMin(Node<T>? node)
    {
        if (node == null) return null;
        while (node.Left != null)
        {
            node = node.Left;
        }
        return node;
    }

    public void Delete(T data)
    {
        var currentNode = Search(Root, data);
        if (currentNode == null)
        {
            return;
        }
        if (currentNode.Right == null && currentNode.Left == null)
        {
            Node<T>? currentParent = currentNode.Parent;
            if (data.CompareTo(currentParent!.Data) > 0)
            {
                currentParent.Right = null;
            }
            else
            {
                currentParent.Left = null;
            }
        }
        else if ((currentNode.Right != null && currentNode.Left == null) || (currentNode.Left != null && currentNode.Right == null))
        {
            if (currentNode.Right != null)
            {
                currentNode.Data = currentNode.Right.Data;
                currentNode.Right = null;
            }
            else
            {
                currentNode.Data = currentNode.Left!.Data;
                currentNode.Left = null;
            }
        }
        else
        {
            
            Node<T> successor = Successor(currentNode)!;
            Node<T>? successorsChild = successor.Right;
            currentNode.Data = successor.Data;
            currentNode.Count = successor.Count;

            if (successor.Parent!.Left == successor) successor.Parent.Left = successorsChild;
            else successor.Parent.Right = successorsChild;
            if (successorsChild != null) successorsChild.Parent = successor.Parent;
            
        }
    }

    public bool IsBalanced()
    {
        if (Root == null) return true;
        
        int rightHeight = Height(Root.Right);
        int leftHeight = Height(Root.Left);

        if (Math.Abs(rightHeight - leftHeight) > 1)
        {
            return false;
        }
        return true;
    }

    private int Height(Node<T>? node)
    {
        if (node == null) return -1;
        return 1 + Math.Max(Height(node.Left), Height(node.Right));
    }
    
    public void PrintTree()
    {
        if (Root == null)
        {
            return;
        }
        PrintTree(Root, "", true);
    }

    private void PrintTree(Node<T>? node, string prefix, bool isRight)
    {
        if (node == null)
            return;

        PrintTree(node.Right, prefix + (isRight ? "    " : "│   "), true);

        Console.WriteLine(prefix + (isRight ? "┌── " : "└── ") + node.Data);

        PrintTree(node.Left, prefix + (isRight ? "│   " : "    "), false);
    }

    public List<T> InOrder(Node<T>? node, List<T> results)
    {
        
        if (node.Left != null)
        {
            InOrder(node.Left, results);
        }
        results.Add(node.Data);
        if (node.Right != null)
        {
            InOrder(node.Right, results);
        }
        return results;
    }

    public List<T> PreOrder(Node<T>? node, List<T> results)
    {
        results.Add(node.Data);
        if (node.Left != null)
        {
            PreOrder(node.Left, results);
        }

        if (node.Right != null)
        {
            PreOrder(node.Right, results);
        }

        return results;
    }

    public List<T> PostOrder(Node<T>? node, List<T> results)
    {
        if (node.Left != null)
        {
            PostOrder(node.Left, results);
        }

        if (node.Right != null)
        {
            PostOrder(node.Right, results);
        }
        results.Add(node.Data);
        
        return results;
    }

    public List<T> InOrderIterative()
    {
        List<T> results = new List<T>();
        Node<T> min = BinarySearchTree<T>.FindMin(Root);
        results.Add(min.Data);
        var x = Successor(min);
        while (x != null)
        {
            results.Add(x.Data);
            x = Successor(x);
        }
        return results;
    }
    
    public void RotateLeft(Node<T> pivot)
    {
        if (pivot.Right == null)
        {
            return;
        }

        Node<T> newRoot = pivot.Right;
        Node<T>? newRootLeft = newRoot.Left;
        Node<T>? pivotParent = pivot.Parent;
        newRoot.Left = pivot;
        pivot.Parent = newRoot;
        pivot.Right = newRootLeft;
        if (newRootLeft != null)
        {
            newRootLeft.Parent = pivot;
        }

        if (pivotParent != null)
        {
            if (pivotParent.Left == pivot)
            {
                pivotParent.Left = newRoot;
            }
            else
            {
                pivotParent.Right = newRoot;
            }
        }
        else
        {
            Root = newRoot;
        }

        newRoot.Parent = pivotParent;
        
    }

    public void RotateRight(Node<T> pivot)
    {
        if (pivot.Left == null)
        {
            return;
        }

        Node<T> newRoot = pivot.Left;
        Node<T>? newRootRight = newRoot.Right;
        Node<T>? pivotParent = pivot.Parent;
        newRoot.Right = pivot;
        pivot.Parent = newRoot;
        pivot.Left = newRootRight;
        if (newRootRight != null)
        {
            newRootRight.Parent = pivot;
        }

        if (pivotParent != null)
        {
            if (pivotParent.Left == pivot)
            {
                pivotParent.Left = newRoot;
            }
            else
            {
                pivotParent.Right = newRoot;
            }
        }
        else
        {
            Root = newRoot;
        }
        

        newRoot.Parent = pivotParent;
    }
}
