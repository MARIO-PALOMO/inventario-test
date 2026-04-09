import { ChangeDetectorRef, Component, inject, OnInit, signal } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Category } from '../../entities/category';
import { NgxSpinnerService } from 'ngx-spinner';
import { ProductService } from '../../services/product.service';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Product } from '../../entities/product';

@Component({
  selector: 'app-editor',
  standalone: false,
  templateUrl: './editor.html',
  styleUrl: './editor.scss',
})
export class EditorProductsComponent implements OnInit {

  private messageService = inject(MessageService);
  categories: Category[] = [];
  productForm: FormGroup | any;
  product: Product | any;
  type: number = 0;


  constructor(private spinner: NgxSpinnerService,
    private service: ProductService,
    private cd: ChangeDetectorRef,
    private fb: FormBuilder,
    private ref: DynamicDialogRef,
    public config: DynamicDialogConfig
  ) { }

  ngOnInit(): void {
    this.getCategories();
    this.productForm = this.fb.group({
      id: [''],
      name: [null, Validators.required],
      description: [null],
      categoryId: [null, Validators.required],
      image: ['', Validators.required],
      price: [null, [Validators.required, Validators.min(1)]],
      stock: [null, [Validators.required, Validators.min(1)]],
      creationDate: [''],
      modificationDate: ['']
    });

    this.type = this.config.data.type;
    if (this.config.data.product != null) {
      this.product = this.config.data.product;
      this.productForm.patchValue(this.product);
    }

  }

  getCategories() {
    this.spinner.show();
    this.service.getCategories().subscribe({
      next: (res) => {
        this.spinner.hide();
        if (res.status) {
          this.categories = res.data;

        } else {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al obtener categorias' });
        }

        this.cd.detectChanges();
      },
      error: (err) => {
        this.spinner.hide();
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al obtener categorias' });
        console.error('Error:', err);
      }
    });
  }

  getImage(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];

    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        const base64String = reader.result as string;
        const onlyBase64 = base64String.split(',')[1];
        this.productForm.patchValue({
          image: onlyBase64
        });
      };

      reader.readAsDataURL(file);
    }
  }

  management(){
    if(this.type == 1){
      this.add();
    }else if(this.type == 2){
      this.update();
    }
  }

  add() {

    if (this.productForm.valid) {

      var { creationDate, id, modificationDate, ...data } = this.productForm.value;

      this.spinner.show();
      this.service.addProduct(data).subscribe({
        next: (res) => {
          this.spinner.hide();
          if (res.status) {

            this.messageService.add({ severity: 'success', summary: 'Éxito', detail: res.message });
            this.ref.close();
          } else {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al guardar producto' });
          }

          this.cd.detectChanges();
        },
        error: (err) => {
          this.spinner.hide();
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al guardar producto' });
          console.error('Error:', err);
        }
      });
    }
  }

  update() {

    if (this.productForm.valid) {

      var { creationDate, modificationDate, ...data } = this.productForm.value;

      this.spinner.show();
      this.service.updateProduct(data).subscribe({
        next: (res) => {
          this.spinner.hide();
          if (res.status) {

            this.messageService.add({ severity: 'success', summary: 'Éxito', detail: res.message });
            this.ref.close();
          } else {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al modificar producto' });
          }

          this.cd.detectChanges();
        },
        error: (err) => {
          this.spinner.hide();
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al modificar producto' });
          console.error('Error:', err);
        }
      });
    }
  }

}
