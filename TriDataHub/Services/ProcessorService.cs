using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using TriDataHub.Classes.Processor;
using TriDataHub.Services.Contracts;
using PuppeteerSharp;

namespace TriDataHub.Services
{
    public class ProcessorService : IProcessorService
    {
        private string _topoCentrasProcesorsBaseUrl = @"https://www.topocentras.lt/kompiuteriai-ir-plansetes/kompiuteriu-komponentai/procesoriai.html";
        private string _topoCentrasLimit = @"?limit=80";
        private string _topoCentrasPage = @"&p=";

        public async Task<List<Processor>> ScrapeTopoProcesors()
        {
            var processors = new List<Processor>();
            var pageCounter = 1;

            var basePage = await ScrapeJavaScriptPage($"{_topoCentrasProcesorsBaseUrl}{_topoCentrasLimit}{_topoCentrasPage}{pageCounter}");
            processors.AddRange(GetProcessorsFromPage(basePage));

            return processors;
        }

        public IEnumerable<Processor> GetProcessorsFromPage(string page)
        {
            var processors = new List<Processor>();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(page);

            var elements = htmlDocument.QuerySelectorAll("article .ProductGridItem-productWrapper-2ip");

            foreach (var element in elements)
            {
                var price = element.QuerySelector(".Price-price-27p").InnerText;
                var name = element.QuerySelector(".ProductGridItem-productName-3ZD").InnerText;
                var pictureUrl = element.QuerySelector(".ProductGridItem-imageContainer-pMi").FirstChild.GetAttributeValue("src", string.Empty);

                var processorListing = new Processors(name, pictureUrl, price);

                processors.Add(processorListing);
            }

            return processors;
        }

        private int GetPageLimit(string page)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(page);

            var elements = htmlDocument.QuerySelectorAll(".Count-pageCount-LOv > span");

            if (int.TryParse(elements[2].InnerHtml, out var pageLimit))
            {
                var pageCounter = 1;
                pageLimit -= 80;

                while (pageLimit > 0)
                {
                    pageLimit -= 80;
                    pageCounter++;
                }

                return pageCounter;
            }

            return 1;
        }

        public async Task<string> ScrapeJavaScriptPage(string url)
        {
            await new BrowserFetcher(Product.Chrome).DownloadAsync();
            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Product = Product.Chrome
            });

            using var page = await browser.NewPageAsync();

            await page.SetViewportAsync(new ViewPortOptions { Width = 1300, Height = 3000 });
            await page.GoToAsync(url, new NavigationOptions { WaitUntil = new WaitUntilNavigation[] { WaitUntilNavigation.Load } });

            return await page.GetContentAsync();
        }
    }
}
