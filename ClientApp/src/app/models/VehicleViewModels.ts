import { ContactViewModel } from "./ContactViewModel";

export class SaveVehicleViewModel {
    id!: number;
    modelId!: number;
    isRegistered!: boolean;
    contact!: ContactViewModel;
    features!: number[];
}