namespace Entity_Framework_Core.Data
{
    public class Currencies
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<BookPrices> BookPrices { get; set; }
    }
}
