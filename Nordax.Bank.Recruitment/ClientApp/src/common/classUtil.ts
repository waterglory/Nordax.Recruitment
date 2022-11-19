
export const nameof = <T>(name: keyof T) => name;

export interface hasIndexer {
    [key: string]: any
}

export const isOfType = <T extends hasIndexer>(obj: T, key: keyof T, type: string) => {
    return typeof obj[key] === type;
}