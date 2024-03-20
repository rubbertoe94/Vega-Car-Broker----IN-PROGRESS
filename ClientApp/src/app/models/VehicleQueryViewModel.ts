export class VehicleQueryViewModel {
    makeId?: number | null;
    modelId?: number | null;
    sortBy?: string;
    isSortAscending?: boolean;
    page!: number;
    pageSize!: number;
}