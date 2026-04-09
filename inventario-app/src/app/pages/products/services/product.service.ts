
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../entities/category';
import { ApiService } from '../../../services/api.service';
import { environment } from '../../../../environments/environment';
import { IResponseModel } from '../../../models/response.model';
import { ProductDTO } from '../entities/productDTO';
import { Product } from '../entities/product';

@Injectable({
    providedIn: 'root'
})
export class ProductService {

    constructor(private api: ApiService) { }

    getCategories(): Observable<IResponseModel<Category[]>> {
        return this.api.get<IResponseModel<Category[]>>(environment.api.paths.products.categories);
    }

    getProducts(filtros: string, pageNumber: number, limit: number): Observable<IResponseModel<ProductDTO[]>> {
        return this.api.get<IResponseModel<ProductDTO[]>>(`${environment.api.paths.products.list}${filtros}&pageNumber=${pageNumber}&pageSize=${limit}`);
    }

    addProduct(product: Product): Observable<IResponseModel<boolean>> {
        return this.api.post<IResponseModel<boolean>>(environment.api.paths.products.add, product);
    }

    updateProduct(product: Product): Observable<IResponseModel<boolean>> {
        return this.api.put<IResponseModel<boolean>>(environment.api.paths.products.update, product);
    }

    deleteProduct(id: any): Observable<IResponseModel<boolean>> {
        return this.api.delete<IResponseModel<boolean>>(`${environment.api.paths.products.delete}${id}`);
    }
}