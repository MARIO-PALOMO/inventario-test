import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadChildren: () =>
            import('./pages/home/home.module').then(
                (m) => m.HomeModule
            ),
    },
    {
        path: 'products',
        loadChildren: () =>
            import('./pages/products/products.module').then(
                (m) => m.ProductsModule
            ),
    },
    {
        path: 'transactions',
        loadChildren: () =>
            import('./pages/transactions/transactions.module').then(
                (m) => m.TransactionsModule
            ),
    }
];
