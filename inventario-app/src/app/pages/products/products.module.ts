
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductsRoutingModule } from './products-routing.module';
import { ListProductsComponent } from './components/list/list';
import { SharedModule } from '../shared/shared.module';
import { NgxSpinnerModule } from "ngx-spinner";
import { ProductService } from './services/product.service';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { SelectModule } from 'primeng/select';
import { EditorProductsComponent } from './components/editor/editor';
import { DialogService } from 'primeng/dynamicdialog';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';


@NgModule({
    declarations: [
        ListProductsComponent,
        EditorProductsComponent
    ],
    imports: [
        SharedModule,
        CommonModule,
        ProductsRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        NgxSpinnerModule,
        ToastModule,
        ButtonModule,
        TableModule,
        SelectModule,
        InputTextModule,
        InputNumberModule
    ],
    providers: [ProductService, MessageService, DialogService],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ProductsModule { }
