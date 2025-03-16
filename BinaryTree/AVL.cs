namespace BinaryTree;

public class AVLNode<T>
{
    public T Data;
    public AVLNode<T>? Left, Right, Parent;
    public int Count;
    public int Height;

    public AVLNode(T data)
    {
        Data = data;
        Count = 1;
        Parent = null;
        Left = null;
        Right = null;
        Height = 0;
    }

    public override string ToString()
    {
        return $"Data:{Data} Height:{Height} Left:{Left?.Data?.ToString()} Right:{Right?.Data?.ToString()} Parent:{Parent?.Data?.ToString()} Count:{Count}";
    }
}

public class AVLTree<T> where T : IComparable<T>
{
    public AVLNode<T>? Root;
    
    public AVLTree()
    {
        Root = null;
    }
    
    public AVLNode<T>? Search(AVLNode<T>? current, T data)
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
        AVLNode<T>? duplicated = Search(Root, data);
        if (duplicated != null)
        {
            duplicated.Count++;
            return;
        }
        AVLNode<T> newNode = new AVLNode<T>(data);
        if (Root == null)
        {
            Root = newNode;
            return;
        }
        AVLNode<T>? currentNode = Root;
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
        UpdateHeights(newNode);
        Balance(newNode);
    }
    
    public static AVLNode<T>? Predecessor(AVLNode<T> node) {
        if (node.Left != null) {
            return FindMax(node.Left);
        }

        AVLNode<T> currentNode = node;
        AVLNode<T>? currentParent = node.Parent;

        while (currentParent != null) {
            if (currentNode == currentParent.Right) break;
            currentNode = currentParent;
            currentParent = currentNode.Parent;
        }
        return currentParent;
    }

    public static AVLNode<T>? Successor(AVLNode<T> node)
    {
        if (node.Right != null)
        {
            return FindMin(node.Right);
        }
        AVLNode<T> currentNode = node;
        AVLNode<T>? currentParent = node.Parent;

        while (currentParent != null)
        {
            if (currentNode == currentParent.Left) break;
            currentNode = currentParent;
            currentParent = currentNode.Parent;
        }
        return currentParent;
    }

    public static AVLNode<T>? FindMax(AVLNode<T>? node) {
        if (node == null) return null;
        while (node.Right != null) {
            node = node.Right;
        }
        return node;
    }

    public static AVLNode<T>? FindMin(AVLNode<T>? node)
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
            AVLNode<T>? currentParent = currentNode.Parent;
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
            
            AVLNode<T> successor = Successor(currentNode)!;
            AVLNode<T>? successorsChild = successor.Right;
            currentNode.Data = successor.Data;
            currentNode.Count = successor.Count;

            if (successor.Parent!.Left == successor) successor.Parent.Left = successorsChild;
            else successor.Parent.Right = successorsChild;
            if (successorsChild != null) successorsChild.Parent = successor.Parent;
            
        }
        UpdateHeights(currentNode);
        Balance(currentNode);
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

    public int Height(AVLNode<T>? node)
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

    private void PrintTree(AVLNode<T>? node, string prefix, bool isRight)
    {
        if (node == null)
            return;

        PrintTree(node.Right, prefix + (isRight ? "    " : "│   "), true);

        Console.WriteLine(prefix + (isRight ? "┌── " : "└── ") + node.Data + ":" + node.Height);

        PrintTree(node.Left, prefix + (isRight ? "│   " : "    "), false);
    }

    public List<T> InOrder(AVLNode<T>? node, List<T> results)
    {
        
        if (node?.Left != null)
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

    public List<T> PreOrder(AVLNode<T>? node, List<T> results)
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

    public List<T> PostOrder(AVLNode<T>? node, List<T> results)
    {
        if (node?.Left != null)
        {
            PostOrder(node.Left, results);
        }

        if (node?.Right != null)
        {
            PostOrder(node.Right, results);
        }
        results.Add(node.Data);
        
        return results;
    }

    public List<T> InOrderIterative()
    {
        List<T> results = new List<T>();
        AVLNode<T>? min = AVLTree<T>.FindMin(Root);
        results.Add(min.Data);
        var x = Successor(min);
        while (x != null)
        {
            results.Add(x.Data);
            x = Successor(x);
        }
        return results;
    }
    
    private void RotateLeft(AVLNode<T> pivot)
    {
        if (pivot.Right == null)
        {
            return;
        }

        AVLNode<T> newRoot = pivot.Right;
        AVLNode<T>? newRootLeft = newRoot.Left;
        AVLNode<T>? pivotParent = pivot.Parent;
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
        UpdateHeights(newRoot);
    }

    private void RotateRight(AVLNode<T> pivot)
    {
        if (pivot.Left == null)
        {
            return;
        }

        AVLNode<T> newRoot = pivot.Left;
        AVLNode<T>? newRootRight = newRoot.Right;
        AVLNode<T>? pivotParent = pivot.Parent;
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
        UpdateHeights(newRoot);
    }

    private void UpdateHeights(AVLNode<T>? node)
    {
            while (node != null)
            {
                node.Height = Height(node);
                node = node.Parent;
            }
    }

    public void Balance(AVLNode<T>? currentNode)
    {
        while (currentNode != null)
        {
            if ((currentNode.Left?.Height ?? -1) - (currentNode.Right?.Height ?? -1) > 1)
            {
                if ((currentNode.Left?.Left?.Height ?? -1) >= (currentNode.Left?.Right?.Height ?? -1))
                {
                    RotateRight(currentNode);
                }
                else
                {
                    RotateLeft(currentNode.Left!);
                    RotateRight(currentNode);
                }
            }
            else if ((currentNode.Left?.Height ?? -1) - (currentNode.Right?.Height ?? -1) < -1)
            {
                if ((currentNode.Right?.Right?.Height ?? -1) >= (currentNode.Right?.Left?.Height ?? -1))
                {
                    RotateLeft(currentNode);
                }
                else
                {
                    RotateRight(currentNode.Right!);
                    RotateLeft(currentNode);
                }
            }
            UpdateHeights(currentNode);
            currentNode = currentNode.Parent;
        }
    }
    
    public void IntegrityCheck(AVLNode<T>? node)
    {
        if (node == null)
        {
            return;
        }
        if (node.Left != null)
        {
            if (node.Left.Parent != node)
            {
                Console.WriteLine("Integrity check failed");
                return;
            }
            IntegrityCheck(node.Left);
        }

        if (node.Right != null)
        {
            if (node.Right.Parent != node)
            {
                Console.WriteLine("Integrity check failed");
                return;
            }
            IntegrityCheck(node.Right);
        }
    }
}
