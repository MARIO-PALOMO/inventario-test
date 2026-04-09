import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { TransactionType } from '../../entities/transaction-type';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Transaction } from '../../entities/transaction';
import { NgxSpinnerService } from 'ngx-spinner';
import { TransactionService } from '../../services/transaction.service';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ProductDTO } from '../../../products/entities/productDTO';

@Component({
  selector: 'app-editor',
  standalone: false,
  templateUrl: './editor.html',
  styleUrl: './editor.scss',
})
export class EditorTransactionsComponent implements OnInit {
  private messageService = inject(MessageService);
  transactionTypes: TransactionType[] = [];
  products: ProductDTO[] = [];
  transactionForm: FormGroup | any;
  transaction: Transaction | any;
  type: number = 0;

  constructor(private spinner: NgxSpinnerService,
    private service: TransactionService,
    private cd: ChangeDetectorRef,
    private fb: FormBuilder,
    private ref: DynamicDialogRef,
    public config: DynamicDialogConfig
  ) { }

  ngOnInit(): void {
    this.getTransactionTypes();
    this.transactionForm = this.fb.group({
      id: [''],
      transactionTypeId: [null, Validators.required],
      description: [null],
      productId: [null, Validators.required],
      amount: [null, [Validators.required, Validators.min(1)]],
      unitPrice: [null, [Validators.required, Validators.min(1)]],
      totalPrice: [null, [Validators.required, Validators.min(1)]],
      detail: [null, Validators.required],
      creationDate: [''],
      modificationDate: ['']
    });

    this.type = this.config.data.type;
    if (this.config.data.transaction != null) {
      this.transaction = this.config.data.transaction;
      this.transactionForm.patchValue(this.transaction);
    }

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

        this.getProducts();
        console.log(res)
      },
      error: (err) => {
        this.spinner.hide();
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al obtener tipo de transacciones' });
        console.error('Error:', err);
      }
    });
  }

  getProducts() {
    this.spinner.show();
    this.service.getProducts().subscribe({
      next: (res) => {
        this.spinner.hide();
        if (res.status) {
          this.products = res.data;

        } else {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al obtener productos' });
        }
        this.cd.detectChanges();

        console.log(res)
      },
      error: (err) => {
        this.spinner.hide();
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al obtener productos' });
        console.error('Error:', err);
      }
    });
  }

  calculatePriceTotal() {
    var id = this.transactionForm.get('productId')?.value;
    var amount = this.transactionForm.get('amount')?.value;
    if ((id != null || id != undefined) && (amount != null || amount != undefined)) {
      this.spinner.show();
      this.service.getProductById(id).subscribe({
        next: (res) => {
          this.spinner.hide();
          this.cd.detectChanges();

          var product: ProductDTO = res.data;
          var total = product.price * parseFloat(amount);

          this.transactionForm.patchValue({
            unitPrice: product.price,
            totalPrice: Math.round(total * 100) / 100
          });


        },
        error: (err) => {
          this.spinner.hide();
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al obtener producto' });
          console.error('Error:', err);
        }
      });
    } else {
      this.transactionForm.patchValue({
        unitPrice: 0,
        totalPrice: 0
      });

    }

  }

  management() {
    if (this.type == 1) {
      this.add();
    } else if (this.type == 2) {
      this.update();
    }
  }

  add() {

    if (this.transactionForm.valid) {

      var { creationDate, id, modificationDate, description, ...data } = this.transactionForm.value;

      this.spinner.show();
      this.service.addTransaction(data).subscribe({
        next: (res) => {
          this.spinner.hide();
          if (res.status) {

            this.messageService.add({ severity: 'success', summary: 'Éxito', detail: res.message });
            this.ref.close();
          } else {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al guardar transacción' });
          }

          this.cd.detectChanges();
        },
        error: (err) => {
          this.spinner.hide();
          this.messageService.add({ severity: 'error', summary: 'Error', detail: err?.error?.message });
          console.error('Error:', err);
        }
      });
    }
  }

  update() {

    if (this.transactionForm.valid) {

      var { creationDate, modificationDate, description, ...data } = this.transactionForm.value;

      this.spinner.show();
      this.service.updateTransaction(data).subscribe({
        next: (res) => {
          this.spinner.hide();
          if (res.status) {

            this.messageService.add({ severity: 'success', summary: 'Éxito', detail: res.message });
            this.ref.close();
          } else {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al modificar transacción' });
          }

          this.cd.detectChanges();
        },
        error: (err) => {
          this.spinner.hide();
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al modificar transacción' });
          console.error('Error:', err);
        }
      });
    }

  }
}
