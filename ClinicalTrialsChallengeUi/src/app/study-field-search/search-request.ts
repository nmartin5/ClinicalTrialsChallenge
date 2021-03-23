export interface SearchRequest {    
    keywords: string[];
    location?: string | null | undefined;
    statuses?: string[] | null | undefined;
    gender?: string | null | undefined;
    centralContactRequired?: boolean | undefined;
}
