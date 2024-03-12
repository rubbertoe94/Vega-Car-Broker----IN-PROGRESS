import { ErrorHandler, Injectable, Injector, NgZone } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class AppErrorHandler implements ErrorHandler {
    private toastr!: ToastrService;

    constructor(
        private ngZone: NgZone,
        private injector: Injector) {}

    handleError(error: any): void {
        this.ngZone.run(() => {
            this.toastr = this.injector.get(ToastrService);
            this.toastr.error('An unexpected error happened', 'Error');
            console.log('An unexpected error happened', error);
        });
        this.toastr = this.injector.get(ToastrService);
    }
}