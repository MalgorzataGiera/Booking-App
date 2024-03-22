namespace ReservationApp.Models
{
    /// <summary>
    /// Represents a paginated list of items.
    /// </summary>
    /// <typeparam name="T">The type of items in the list.</typeparam>
    public class PagingList<T>
    {
        /// <summary>
        /// Gets the data of the current page.
        /// </summary>
        public IEnumerable<T> Data { get; }

        /// <summary>
        /// Gets the total number of items in the entire collection.
        /// </summary>
        public int TotalItems { get; }

        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Gets the current page number.
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// Gets the page size.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gets a value indicating whether the current page is the first page.
        /// </summary>
        public bool IsFirst { get; }

        /// <summary>
        /// Gets a value indicating whether the current page is the last page.
        /// </summary>
        public bool IsLast { get; }

        /// <summary>
        /// Gets a value indicating whether there is a next page.
        /// </summary>
        public bool IsNext { get; }

        /// <summary>
        /// Gets a value indicating whether there is a previous page.
        /// </summary>
        public bool IsPrevious { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagingList{T}"/> class.
        /// </summary>
        /// <param name="dataGenerator">A function to generate the data for the page.</param>
        /// <param name="totalItems">The total number of items in the entire collection.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="size">The page size.</param>
        private PagingList(Func<int, int, IEnumerable<T>> dataGenerator, int totalItems, int page, int size)
        {
            TotalItems = totalItems;
            Page = page;
            Size = size;
            TotalPages = CalcTotalPages(Page, TotalItems, Size);
            IsFirst = Page <= 1;
            IsLast = Page >= TotalPages;
            IsNext = !IsLast;
            IsPrevious = !IsFirst;
            Data = dataGenerator.Invoke(Page, size);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="PagingList{T}"/> class.
        /// </summary>
        /// <param name="data">A function to generate the data for the page.</param>
        /// <param name="totalItems">The total number of items in the entire collection.</param>
        /// <param name="number">The current page number.</param>
        /// <param name="size">The page size.</param>
        /// <returns>A new instance of <see cref="PagingList{T}"/>.</returns>
        public static PagingList<T> Create(Func<int, int, IEnumerable<T>> data, int totalItems, int number, int size)
        {
            return new PagingList<T>(data, totalItems, ClipPage(number, totalItems, size), Math.Abs(size));
        }

        /// <summary>
        /// Calculates the total number of pages based on the number of items, page size, and current page number.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="totalItems">The total number of items.</param>
        /// <param name="size">The page size.</param>
        /// <returns>The total number of pages.</returns>
        private static int CalcTotalPages(int page, int totalItems, int size)
        {
            return totalItems / size + (totalItems % size > 0 ? 1 : 0);
        }

        /// <summary>
        /// Clips the page number to ensure it does not exceed the total number of pages.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="totalItems">The total number of items.</param>
        /// <param name="size">The page size.</param>
        /// <returns>The corrected page number.</returns>
        private static int ClipPage(int page, int totalItems, int size)
        {
            int totalPages = CalcTotalPages(page, totalItems, size);
            if (page > totalPages)
            {
                return totalPages;
            }
            if (page < 1)
            {
                return 1;
            }
            return page;
        }
    }
}
