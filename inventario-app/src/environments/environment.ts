export const environment = {
    production: false,
    api: {
        url: 'https://localhost:44370/api/v1',
        paths: {
            products: {
                categories: 'Product/Category/GetAll',
                list: 'Product/Get?',
                listAll: 'Product/GetAll',
                listById: 'Product/GetById?id=',
                add: 'Product/Add',
                update: 'Product/Update',
                delete: 'Product/Delete?id=',
            },
            transaction: {
                transactionype: 'Transaction/Type/GetAll',
                list: 'Transaction/Get?',
                listGroupByProduct: 'Transaction/GetGroupByProduct?',
                add: 'Transaction/Add',
                update: 'Transaction/Update',
                delete: 'Transaction/Delete?id='
            }
        }
    }
}