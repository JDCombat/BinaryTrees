namespace BinaryTree;

public class Program
{
    static void Main(string[] args)
    {
            // BinarySearchTree<int> tree = new BinarySearchTree<int>();
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(30);
            tree.Insert(10);
            tree.Insert(20);
            tree.Insert(40);
            tree.Insert(480);
            tree.Insert(5);
            tree.Insert(4);
            tree.Insert(2);
            tree.Insert(1);
            tree.Insert(6);
            tree.Insert(2137);
            tree.Insert(35);
            tree.Insert(3000);
            tree.Insert(321);
            tree.Insert(29);
            tree.Insert(3);

            
            // Console.WriteLine(tree.Search(tree.Root,480)!.Left);
            // Console.WriteLine(tree.Search(tree.Root,480)!.Right!.Data);

            // Console.WriteLine(tree.Search(tree.Root,480)!.Right!.Data);
            
            // tree.Delete(40);
            // tree.Delete(3000);

            tree.PrintTree();

            Console.WriteLine(tree.Root!.Left);

            tree.InOrder(tree.Root!, new List<int>()).ForEach(i => Console.Write(i + " "));
            Console.WriteLine();
            tree.PreOrder(tree.Root!, new List<int>()).ForEach(i => Console.Write(i + " "));
            Console.WriteLine();
            tree.PostOrder(tree.Root!, new List<int>()).ForEach(i => Console.Write(i + " "));
            Console.WriteLine();
            tree.InOrderIterative().ForEach(i => Console.Write(i + " "));

            Console.WriteLine("\n \n");
            // tree.RotateRight(tree.Search(tree.Root, 10)!);
            // tree.RotateRight(tree.Root!);
            // tree.PrintTree();

            // Console.WriteLine(tree.Search(tree.Root, 480)!.Height);
            // Console.WriteLine(tree.Height(tree.Search(tree.Root, 480)));


            Console.WriteLine(tree.Height(tree.Search(tree.Root,3)));
            // Console.WriteLine(tree.Height(tree.Search(tree.Root,35)));
            
            // tree.IntegrityCheck(tree.Root);
            
            // tree.RotateLeft(tree.Root!.Left!);
            // tree.RotateRight(tree.Root!);
            
            
            tree.PrintTree();
            tree.Delete(3);
            tree.PrintTree();
            
            
    }
}