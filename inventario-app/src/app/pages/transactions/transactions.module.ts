
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { NgxSpinnerModule } from "ngx-spinner";
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { SelectModule } from 'primeng/select';
import { DialogService } from 'primeng/dynamicdialog';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { ListTransactionsComponent } from './components/list/list';
import { TransactionsRoutingModule } from './transactions-routing.module';
import { TransactionService } from './services/transaction.service';
import { DatePickerModule } from 'primeng/datepicker';
import { EditorTransactionsComponent } from './components/editor/editor';
import { TabsModule } from 'primeng/tabs';


@NgModule({
    declarations: [
        ListTransactionsComponent,
        EditorTransactionsComponent
    ],
    imports: [
        SharedModule,
        CommonModule,
        TransactionsRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        NgxSpinnerModule,
        ToastModule,
        ButtonModule,
        TableModule,
        SelectModule,
        InputTextModule,
        InputNumberModule,
        DatePickerModule,
        TabsModule
    ],
    providers: [TransactionService, MessageService, DialogService],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class TransactionsModule { }
