import { ContactViewModel } from "./ContactViewModel";

export interface KeyValuePair {
    id: number;
    name: string;
}

export class VehicleViewModel {
    id!: number;
    model!: KeyValuePair;
    make!: KeyValuePair;
    isRegistered!: boolean;
    contact!: ContactViewModel;
    features!: KeyValuePair[];
    lastUpdate!: string;
}
export class SaveVehicleViewModel {
    id!: number;
    modelId!: number;
    makeId!: number;
    isRegistered!: boolean;
    contact!: ContactViewModel;
    features!: number[];
}