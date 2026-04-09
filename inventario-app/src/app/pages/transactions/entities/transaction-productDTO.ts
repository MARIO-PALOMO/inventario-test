export interface TransactionProductDTO {
    name: string;
    currentStock: number;
    transactionType: string;
    totalTransactionType: number;
    totalPurchasesAmount: number;
    totalSalesAmount: number;
    totalPurchasesPrice: number;
    totalSalesPrice: number;
    start: string,
    end: string
}