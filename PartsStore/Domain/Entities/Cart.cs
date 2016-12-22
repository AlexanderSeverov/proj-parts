using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }

        public void AddItem(Part part, int quantity)
        {
            CartLine line = lineCollection
                .Where(b => b.Part.PartId == part.PartId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine { Part = part, Quantity = quantity });

            }
            else
            {
                line.Quantity += quantity;

            }

        }

            public void RemoveLine(Part part)
        {
            lineCollection.RemoveAll(l => l.Part.PartId == part.PartId);
        }

        public decimal ComputerTotalValue()
        {
            return lineCollection.Sum(e => e.Part.Price * e.Quantity);
         }

        public void Clear()
        {

            lineCollection.Clear();
        }

        
    }
    
    public class CartLine
    {
        public Part Part { get; set; }
        public int Quantity { get; set; }
    }
}
