using TriDataHub.Classes.Processor;

namespace TriDataHub.Models
{
    public class ProcessorModel
    {
        public int Page { get; set; } = 1;

        public List<Processor> Processor { get; set; }

        public ProcessorModel(List<Processor> processor)
        {
            Processor = processor;
        }

        public ProcessorModel(List<Processor> processor, int page)
        {
            Processor = processor;
            Page = page;
        }
    }
}
