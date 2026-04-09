using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario.Domain.DTOs
{
    public class TransactionProductDTO
    {
        public string Name { get; set; }
        public int CurrentStock { get; set; }
        public string TransactionType { get; set; }
        public int TotalTransactionType { get; set; }
        public decimal TotalPurchasesAmount { get; set; }
        public decimal TotalSalesAmount { get; set; }
        public decimal TotalPurchasesPrice { get; set; }
        public decimal TotalSalesPrice { get; set; }
    }
}
