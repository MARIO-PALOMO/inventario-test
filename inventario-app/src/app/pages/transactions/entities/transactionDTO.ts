import { Product } from "../../products/entities/product";
import { TransactionType } from "./transaction-type";

export interface TransactionDTO {
    transactionId: number;
    code: string;
    date: string;
    transactionTypeId: number;
    transactionType: TransactionType;
    productId: string;
    product: Product;
    amount: number;
    unitPrice: number;
    totalPrice: number;
    detail?: string;
    creationDate: string;
    modificationDate: string;
    start: string,
    end: string
}