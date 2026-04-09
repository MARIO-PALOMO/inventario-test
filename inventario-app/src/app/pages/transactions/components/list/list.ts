import { ChangeDetectorRef, Component, inject, OnInit, ViewChild } from '@angular/core';
import { LazyLoadEvent, MessageService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Table } from 'primeng/table';
import { TransactionType } from '../../entities/transaction-type';
import { TransactionDTO } from '../../entities/transactionDTO';
import { NgxSpinnerService } from 'ngx-spinner';
import { TransactionService } from '../../services/transaction.service';
import { EditorTransactionsComponent } from '../editor/editor';
import { TransactionProductDTO } from '../../entities/transaction-productDTO';

@Component({
  selector: 'app-list',
  standalone: false,
  templateUrl: './list.html',
  styleUrl: './list.scss',
})
export class ListTransactionsComponent implements OnInit {
  @ViewChild('dt2') table!: Table;
  @ViewChild('dt1') table1!: Table;
  private dialogService = inject(DialogService);
  private messageService = inject(MessageService);
  ref: DynamicDialogRef | any;

  transactionTypes: TransactionType[] = [];
  transactionsDTO = {
    transactions: [] as TransactionDTO[],
    total: 0,
    page: 1,
    limit: 10,
    loading: true
  }

  transactionsByProductDTO = {
    transactionsProducts: [] as TransactionProductDTO[],
    total: 0,
    page: 1,
    limit: 10,
    loading: true
  }

  constructor(private spinner: NgxSpinnerService,
    private service: TransactionService,
    private cd: ChangeDetectorRef,
  ) { }

  ngOnInit(): void {
    this.getTransactionTypes();
  }

  getTransactionTypes() {
    this.spinner.show();
    this.service.getTransactionTypes().subscribe({
      next: (res) => {
        this.spinner.hide();
        if (res.status) {
          this.transactionTypes = res.data;

        } else {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al obtener tipo de transacciones' });
        }
        this.cd.detectChanges();

      },
      error: (err) => {
        this.spinner.hide();
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al obtener tipo de transacciones' });
        console.error('Error:', err);
      }
    });
  }

  getTransactions(event: LazyLoadEvent | any) {
    this.transactionsDTO.loading = true;
    var page = (event.first / event.rows) + 1;
    var size = event.rows;

    var today = new Date();
    today.setDate(today.getDate() - 2);

    var tomorrow = new Date();
    tomorrow.setDate(today.getDate() + 2);

    var filter = '';
    var start = event.filters['start']?.value || today;
    var end = event.filters['end']?.value || tomorrow;
    var transactionTypeId = event.filters['transactionTypeId']?.value || null;
    var product = event.filters['product?.name']?.value || null;

    if (transactionTypeId != null) {
      filter += `&transaction=${transactionTypeId}`
    } if (product != null) {
      filter += `&product=${product}`
    }

    var startDate = `${start.toISOString().split('T')[0]} 00:00:00`;
    var endDate = `${end.toISOString().split('T')[0]} 23:59:59`;

    this.service.getTransactions(`start=${startDate}&end=${endDate}${filter}`, page, size).subscribe({
      next: (res) => {
        this.transactionsDTO.loading = false;
        if (res.status) {
          this.transactionsDTO.transactions = res.data;
          this.transactionsDTO.total = res.total;
        } else {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al obtener transacciones' });
        }
        this.cd.detectChanges();
      },
      error: (err) => {
        this.transactionsDTO.loading = false;
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al obtener transacciones' });
        console.error('Error:', err);
      }
    });
  }

  addTransaction() {
    this.ref = this.dialogService.open(EditorTransactionsComponent, {
      data: {
        type: 1,
        transaction: null
      },
      header: 'Agregar Transacción',
      modal: true,
      width: '50vw',
      closable: true,
      contentStyle: { overflow: 'auto' },
      breakpoints: {
        '960px': '75vw',
        '640px': '90vw'
      }
    });

    this.ref.onClose.subscribe((data: any) => {
      this.refreshTable();
    });
  }

  updateTransaction(transaction: any) {

    const { transactionId, ...rest } = transaction;
    const newTransaction = { ...rest, id: transactionId };

    this.ref = this.dialogService.open(EditorTransactionsComponent, {
      data: {
        type: 2,
        transaction: newTransaction
      },
      header: 'Modificar Transacción',
      modal: true,
      width: '50vw',
      closable: true,
      contentStyle: { overflow: 'auto' },
      breakpoints: {
        '960px': '75vw',
        '640px': '90vw'
      }
    });

    this.ref.onClose.subscribe((data: any) => {
      this.refreshTable();
    });
  }

  deleteTransaction(id: any) {
    this.spinner.show();
    this.service.deleteTransaction(id).subscribe({
      next: (res) => {
        this.spinner.hide();
        if (res.status) {
          this.messageService.add({ severity: 'success', summary: 'Éxito', detail: res.message });
          this.refreshTable();
        } else {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al eliminar transacción' });
        }
      },
      error: (err) => {
        this.spinner.hide();
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al eliminar transacción' });
        console.error('Error:', err);
      }
    });
  }


  refreshTable() {
    if (this.table) {
      this.table.reset();
    }
  }

  refreshTableTransactionsByProducts() {
    if (this.table1) {
      this.table1.reset();
    }
  }

  getTransactionsByProducts(event: LazyLoadEvent | any) {
    this.transactionsByProductDTO.loading = true;
    var page = (event.first / event.rows) + 1;
    var size = event.rows;

    var today = new Date();
    today.setDate(today.getDate() - 2);

    var tomorrow = new Date();
    tomorrow.setDate(today.getDate() + 2);

    var filter = '';
    var start = event.filters['start']?.value || today;
    var end = event.filters['end']?.value || tomorrow;
    var transactionTypeId = event.filters['transactionType']?.value || null;

    if (transactionTypeId != null) {
      filter += `&transaction=${transactionTypeId}`
    } 

    var startDate = `${start.toISOString().split('T')[0]} 00:00:00`;
    var endDate = `${end.toISOString().split('T')[0]} 23:59:59`;

    console.log(startDate)
     console.log(endDate)
      console.log(filter)

    this.service.getTransactionsGroupByProduct(`start=${startDate}&end=${endDate}${filter}`, page, size).subscribe({
      next: (res) => {
        this.transactionsByProductDTO.loading = false;
        if (res.status) {
          this.transactionsByProductDTO.transactionsProducts = res.data;
          this.transactionsByProductDTO.total = res.total;
        } else {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al obtener transacciones' });
        }
        this.cd.detectChanges();
      },
      error: (err) => {
        this.transactionsByProductDTO.loading = false;
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al obtener transacciones' });
        console.error('Error:', err);
      }
    });
  }
}
