using System.Collections.Generic;
using System.Linq;

namespace CM.API.Models {
public class OrderResult
{
        public int Id { get; set; } // Primary key

        public int OrderResultsId { get; set; }
        public int OrderId { get; set; }
        public DateTime ProcessedDate { get; set; }
        public bool Success { get; set; }
        public string Details { get; set; }
}
}