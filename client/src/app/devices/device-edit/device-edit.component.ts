import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Device } from 'src/app/_models/device';
import { DevicesService } from 'src/app/_services/devices.service';

@Component({
  selector: 'app-device-edit',
  templateUrl: './device-edit.component.html',
  styleUrls: ['./device-edit.component.css']
})
export class DeviceEditComponent implements OnInit {
  @ViewChild('editDeviceForm') editDeviceForm: NgForm;
  device: Device;

  constructor(private devicesService: DevicesService, private router: ActivatedRoute,
      private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadDevice();
  }

  loadDevice() {
    this.devicesService.getDevice(parseInt(this.router.snapshot.paramMap.get('id'))).subscribe(device => {
      this.device = device;
    })  
  }

  updateDevice() {
    this.devicesService.updateDevice(this.device).subscribe(() => {
      this.toastr.success('Device updated successfully');
      this.editDeviceForm.reset(this.device);
    })
  }

}
