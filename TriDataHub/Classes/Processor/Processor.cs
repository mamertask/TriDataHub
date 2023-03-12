namespace TriDataHub.Classes.Processor
{
    public class Processor
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public Processor(string name, string pictureUrl, string price)
        {
            Name = name;
            PictureUrl = pictureUrl;

            if (decimal.TryParse(price.Replace("€", "").Trim(), out decimal priceDecimal))
            {
                Price = priceDecimal / 100;
            }
            else
            {
                Price = -1;
            }
        }
    }
}
