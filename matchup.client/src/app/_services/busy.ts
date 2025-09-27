import { inject, Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})
export class Busy {
  busyReqCount = 0;
  private spinnerService = inject(NgxSpinnerService);

  busy() {
    this.busyReqCount++;
    this.spinnerService.show(undefined, {
      type: 'ball-atom',
      bdColor: 'rgba(255, 255,255, 0)',
      color: '#333333',
    });
  }

  idle() {
    this.busyReqCount--;
    if (this.busyReqCount <= 0) {
      this.busyReqCount = 0;
      this.spinnerService.hide();
    }
  }
}
