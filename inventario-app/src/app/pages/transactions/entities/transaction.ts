import { Product } from "../../products/entities/product";
import { TransactionType } from "./transaction-type";

export interface Transaction {
    id: number;
    creationDate?: string;
    modificationDate?: string;
    code: string;
    date: string;
    transactionTypeId: number;
    transactionType?: TransactionType;
    productId: string;
    product?: Product;
    amount: number;
    unitPrice: number;
    totalPrice: number;
    detail?: string;
}