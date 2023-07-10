namespace Practice.Domain;

public static class Sorting
{
    public enum SortingMode
    {
        Ascending,
        Descending
    }
    
    public enum SortingMethod
    {
        InsertionSort,
        SelectionSort,
        HeapSort,
        QuickSort,
        MergeSort
    }
    
    // IComparer<T>
    public static T[] MySort<T>(
        this T[] collectionToSort, 
        SortingMode sortingMode, 
        SortingMethod sortingMethod, 
        IComparer<T> comparer)
    {
        CheckCollections(collectionToSort, comparer);
        switch (sortingMethod)
        {
            case SortingMethod.InsertionSort:
                InsertionSorting(collectionToSort, sortingMode, comparer);
                break;
            case SortingMethod.SelectionSort:
                SelectionSorting(collectionToSort, sortingMode, comparer);
                break;
            case SortingMethod.MergeSort:
                MergeSorting(collectionToSort, sortingMode, comparer);
                break;
            case SortingMethod.HeapSort:
                HeapSorting(collectionToSort, sortingMode, comparer);
                break;
            case SortingMethod.QuickSort:
                QuickSorting(collectionToSort, sortingMode, comparer);
                break;
            default:
                throw new ArgumentException(nameof(sortingMethod));
        }

        return collectionToSort;
    }
    
    // параметра нет
    public static T[] MySort<T>(
        this T[] collectionToSort, 
        SortingMode sortingMode, 
        SortingMethod sortingMethod)
        where T : IComparable<T>
    {
        var comparerSort = Comparer<T>.Default;
        return collectionToSort.MySort(sortingMode, sortingMethod, comparerSort);
    }

    // Comparer<T>
    public static T[] MySort<T>(
        this T[] collectionToSort, 
        SortingMode sortingMode, 
        SortingMethod sortingMethod, 
        Comparer<T> comparer)
    {
        
        return collectionToSort.MySort(sortingMode, sortingMethod, comparer as IComparer<T>);
    }
    
    // Comparison<T>
    public static T[] MySort<T>(
        this T[] collectionToSort, 
        SortingMode sortingMode, 
        SortingMethod sortingMethod, 
        Comparison<T> comparison)
    {
        var comparer = Comparer<T>.Create(comparison);
        return collectionToSort.MySort(sortingMode, sortingMethod, comparer);
    }
    
    private static void CheckCollections<T>(T[] collection, IComparer<T> comparer)
    {
        if (collection is null)
            throw new ArgumentNullException( nameof(collection));
        
        if (comparer is null)
            throw new ArgumentNullException(nameof(comparer));
    }
    
    private static void InsertionSorting<T>(T[] collection, SortingMode sortingMode, IComparer<T> comparer)
    {
        for (var i = 1; i < collection.Length; i++)
        {
            for (var j = i; j > 0 && (sortingMode == SortingMode.Ascending
                     ? comparer.Compare(collection[j], collection[j - 1]) < 0
                     : comparer.Compare(collection[j], collection[j - 1]) > 0); j--)
            {
                (collection[j - 1], collection[j]) = (collection[j], collection[j - 1]); // swap подсказка от rider
            }
        }
    }
    
    private static void SelectionSorting<T>(T[] collection, SortingMode sortingMode, IComparer<T> comparer)
    {
        for (var i = 0; i < collection.Length - 1; i++)
        {
            var jmin = i;
            for (var j = i + 1; j < collection.Length; j++)
            {
                if (sortingMode == SortingMode.Ascending
                        ? comparer.Compare(collection[jmin], collection[j]) > 0
                        : comparer.Compare(collection[jmin], collection[j]) < 0)
                {
                    jmin = j;
                }
            }

            (collection[i], collection[jmin]) = (collection[jmin], collection[i]);
        }

    }
    
    private static void HeapSorting<T>(T[] collection, SortingMode sortingMode, IComparer<T> comparer)
    {
        var sortIndex = sortingMode == SortingMode.Ascending? 1 : -1;
        var len = collection.Length;
        
        for (var i = len / 2 - 1; i >= 0; --i)
            Heapify(collection, len, i, comparer, sortIndex);
        
        for (var i = len - 1; i >= 0; --i)
        {
            (collection[0], collection[i]) = (collection[i], collection[0]);

            Heapify(collection, i, 0, comparer, sortIndex);
        }
    }

    private static void Heapify<T>(T[] collection, int len, int i, IComparer<T> comparer, int sortIndex)
    {
        var target = i;
        var left = 2 * i + 1;
        var right = 2 * i + 2;
        
        if (left < len && comparer.Compare(collection[left], collection[target]) == sortIndex)
            target = left;
        
        if (right < len && comparer.Compare(collection[right], collection[target]) == sortIndex)
            target = right;

        if (target == i) return;
        (collection[i], collection[target]) = (collection[target], collection[i]);

        Heapify(collection, len, target, comparer, sortIndex);
    }

    private static void QuickSorting<T>(T[] collection, SortingMode sortingMode, IComparer<T> comparer)
    {
        var sortIndex = sortingMode == SortingMode.Ascending ? -1 : 1;
        QuickSortRecursion(collection, 0, collection.Length - 1, comparer, sortIndex);
    }
    
    private static void QuickSortRecursion<T>(T[] collection, int minIndex, int maxIndex, IComparer<T> comparer, int sortIndex)
    {
        if (minIndex >= maxIndex) return;

        var pivotIndex = Partition(collection, minIndex, maxIndex, comparer, sortIndex);
        QuickSortRecursion(collection, minIndex, pivotIndex - 1, comparer, sortIndex);
        QuickSortRecursion(collection, pivotIndex + 1, maxIndex, comparer, sortIndex);
    }
    
    private static int Partition<T>(T[] collection, int minIndex, int maxIndex, IComparer<T> comparer, int sortIndex)
    {
        var pivot = minIndex - 1;
        for (var i = minIndex; i < maxIndex; ++i)
            if (comparer.Compare(collection[i], collection[maxIndex]) == sortIndex)
            {
                pivot++;
                (collection[pivot], collection[i]) = (collection[i], collection[pivot]);
            }
        pivot++;
        (collection[pivot], collection[maxIndex]) = (collection[maxIndex], collection[pivot]);
        return pivot;
    }
    
    private static void MergeSorting<T>(T[] collection, SortingMode sortingMode, IComparer<T> comparer)
    {
        var sortIndex = sortingMode == SortingMode.Ascending ? -1 : 1;
        MergeSortRecursion(collection, 0, collection.Length - 1, comparer, sortIndex);
    }
    
    private static void MergeSortRecursion<T>(T[] array, int lowIndex, int highIndex, IComparer<T> comparer, int sortIndex)
    {
        if (lowIndex >= highIndex) return;
        var middleIndex = (lowIndex + highIndex) / 2;
        MergeSortRecursion(array, lowIndex, middleIndex, comparer, sortIndex);
        MergeSortRecursion(array, middleIndex + 1, highIndex, comparer, sortIndex);
        Merge(array, lowIndex, middleIndex, highIndex, comparer, sortIndex);
    }
    
    private static void Merge<T>(T[] arr, int lowIndex, int middleIndex, int highIndex, IComparer<T> comparer, int sortIndex)
    {
        var left = lowIndex;
        var right = middleIndex + 1;
        var tempArr = new T[highIndex - lowIndex + 1];
        var index = 0;

        while (left <= middleIndex && right <= highIndex)
        {
            if (comparer.Compare(arr[left], arr[right]) == sortIndex)
            {
                tempArr[index] = arr[left];
                left++;
            }
            else
            {
                tempArr[index] = arr[right];
                right++;
            }
            index++;
        }

        for (var i = left; i <= middleIndex; i++)
        {
            tempArr[index] = arr[i];
            index++;
        }

        for (var i = right; i <= highIndex; i++)
        {
            tempArr[index] = arr[i];
            index++;
        }

        for (var i = 0; i < tempArr.Length; i++) 
            arr[lowIndex + i] = tempArr[i];
    }
}