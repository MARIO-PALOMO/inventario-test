export interface IResponseModel<T> {
    total: number;
    message: string;
    status: boolean;
    data: T;
}