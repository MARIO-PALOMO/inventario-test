import { ChangeDetectorRef, Component, inject, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";
import { Category } from '../../entities/category';
import { ProductService } from '../../services/product.service';
import { LazyLoadEvent, MessageService } from 'primeng/api';
import { ProductDTO } from '../../entities/productDTO';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { EditorProductsComponent } from '../editor/editor';
import { Table } from 'primeng/table';
@Component({
  selector: 'app-list',
  standalone: false,
  templateUrl: './list.html',
  styleUrl: './list.scss',
})
export class ListProductsComponent implements OnInit {

  @ViewChild('dt2') table!: Table;
  private dialogService = inject(DialogService);
  private messageService = inject(MessageService);
  ref: DynamicDialogRef | any;

  categories: Category[] = [];
  productsDTO = {
    products: [] as ProductDTO[],
    total: 0,
    page: 1,
    limit: 10,
    loading: true
  }

  constructor(private spinner: NgxSpinnerService,
    private service: ProductService,
    private cd: ChangeDetectorRef,
  ) { }

  ngOnInit(): void {
    this.getCategories();
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

  getProducts(event: LazyLoadEvent | any) {
    this.productsDTO.loading = true;
    var page = (event.first / event.rows) + 1;
    var size = event.rows;

    var filter = '';
    var name = event.filters['name']?.value || null;
    var categoryId = event.filters['categoryId']?.value || null;
    if (name != null) {
      filter += `name=${name}`;
    } if (categoryId != null) {
      filter += `&category=${categoryId}`
    }

    this.service.getProducts(filter, page, size).subscribe({
      next: (res) => {
        this.productsDTO.loading = false;
        if (res.status) {
          this.productsDTO.products = res.data;
          this.productsDTO.total = res.total;
        } else {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al obtener productos' });
        }
        this.cd.detectChanges();
      },
      error: (err) => {
        this.productsDTO.loading = false;
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al obtener productos' });
        console.error('Error:', err);
      }
    });
  }

  addProduct() {
    this.ref = this.dialogService.open(EditorProductsComponent, {
      data: {
        type: 1,
        product: null
      },
      header: 'Agregar Producto',
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

  updateProduct(product: any) {

    const { productId, ...rest } = product;
    const newProduct = { ...rest, id: productId };

    this.ref = this.dialogService.open(EditorProductsComponent, {
      data: {
        type: 2,
        product: newProduct
      },
      header: 'Modificar Producto',
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

  deleteProduct(id: any) {
    this.spinner.show();
    this.service.deleteProduct(id).subscribe({
      next: (res) => {
        this.spinner.hide();
        if (res.status) {
          this.messageService.add({ severity: 'success', summary: 'Éxito', detail: res.message });
          this.refreshTable();
        } else {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al eliminar producto' });
        }
      },
      error: (err) => {
        this.spinner.hide();
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al eliminar producto' });
        console.error('Error:', err);
      }
    });
  }

  refreshTable() {
    if (this.table) {
      this.table.reset();
    }
  }
}
