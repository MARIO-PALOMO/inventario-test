
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../../services/api.service';
import { environment } from '../../../../environments/environment';
import { IResponseModel } from '../../../models/response.model';
import { TransactionType } from '../entities/transaction-type';
import { TransactionDTO } from '../entities/transactionDTO';
import { Transaction } from '../entities/transaction';
import { ProductDTO } from '../../products/entities/productDTO';
import { TransactionProductDTO } from '../entities/transaction-productDTO';


@Injectable({
    providedIn: 'root'
})
export class TransactionService {

    constructor(private api: ApiService) { }

    getTransactionTypes(): Observable<IResponseModel<TransactionType[]>> {
        return this.api.get<IResponseModel<TransactionType[]>>(environment.api.paths.transaction.transactionype);
    }

    getProducts(): Observable<IResponseModel<ProductDTO[]>> {
        return this.api.get<IResponseModel<ProductDTO[]>>(environment.api.paths.products.listAll);
    }

    getProductById(id: any): Observable<IResponseModel<ProductDTO>> {
        return this.api.get<IResponseModel<ProductDTO>>(`${environment.api.paths.products.listById}${id}`);
    }

    getTransactionsGroupByProduct(filtros: string, pageNumber: number, limit: number): Observable<IResponseModel<TransactionProductDTO[]>> {
        return this.api.get<IResponseModel<TransactionProductDTO[]>>(`${environment.api.paths.transaction.listGroupByProduct}${filtros}&pageNumber=${pageNumber}&pageSize=${limit}`);
    }

    getTransactions(filtros: string, pageNumber: number, limit: number): Observable<IResponseModel<TransactionDTO[]>> {
        return this.api.get<IResponseModel<TransactionDTO[]>>(`${environment.api.paths.transaction.list}${filtros}&pageNumber=${pageNumber}&pageSize=${limit}`);
    }

    addTransaction(transaction: Transaction): Observable<IResponseModel<boolean>> {
        return this.api.post<IResponseModel<boolean>>(environment.api.paths.transaction.add, transaction);
    }

    updateTransaction(transaction: Transaction): Observable<IResponseModel<boolean>> {
        return this.api.put<IResponseModel<boolean>>(environment.api.paths.transaction.update, transaction);
    }


    deleteTransaction(id: any): Observable<IResponseModel<boolean>> {
        return this.api.delete<IResponseModel<boolean>>(`${environment.api.paths.transaction.delete}${id}`);
    }
}