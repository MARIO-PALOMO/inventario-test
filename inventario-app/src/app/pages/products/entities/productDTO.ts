import { Category } from "./category";

export interface ProductDTO {
    productId: string;
    name: string;
    description: string;
    categoryId: number;
    category: Category;
    image: string;
    price: number;
    stock: number;
    statusStock: boolean;
    creationDate: string;
    modificationDate: string;

}
