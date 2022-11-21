export interface IHttpClient {
    get<T>(url: string): Promise<T>;

    post<T>(
        url: string,
        data?: any,
        appendHeaders?: Record<string, string>
    ): Promise<T>;

    put<T>(
        url: string,
        data?: any,
        appendHeaders?: Record<string, string>
    ): Promise<T>;

    delete<T>(
        url: string
    ): Promise<T>;

    postFile<T>(
        url: string,
        content?: File,
        appendHeaders?: Record<string, string>
    ): Promise<T>;
}