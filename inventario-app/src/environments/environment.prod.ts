export const environment = {
    production: true,
    api: {
        url: 'https://localhost:44370/api/v1',
        paths: {
            products: {
                categories: 'Product/Category/GetAll',
                list: 'Product/Get?',
                add: 'Product/Add',
                update: 'Product/Update',
                delete: 'Product/Delete?id=',
            },
            transaction: {
                transactionype: 'Transaction/Type/GetAll',
                list: 'Transaction/Get?',
                listGroupByProduct: 'Transaction/GetGroupByProduct?',
                add: 'Transaction/Add',
            }
        }
    }
}