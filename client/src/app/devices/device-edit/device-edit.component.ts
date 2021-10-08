import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
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
  editDeviceForm: FormGroup;
  device: Device;
  deviceTypes: string[];

  constructor(private devicesService: DevicesService, private router: ActivatedRoute,
      private toastr: ToastrService, private fb: FormBuilder) {
      }

  ngOnInit(): void {
    this.loadDevice();
    this.loadDeviceTypes();
  }

  initializeForm(device: Device) {
    this.editDeviceForm = this.fb.group({
      id: [device.id],
      name: [device.name, Validators.required],
      manufacturer: [device.manufacturer, Validators.required],
      type: [device.type, Validators.required],
      os: [device.os, Validators.required],
      osVersion: [device.osVersion, Validators.required],
      processor: [device.processor, Validators.required],
      ram: [device.ram, Validators.required]
    })
  }

  loadDevice() {
    this.devicesService.getDevice(parseInt(this.router.snapshot.paramMap.get('id'))).subscribe(device => {
      this.device = device;
      console.log('loaded');
      console.log(device);

      this.initializeForm(device);
    })  
  }

  loadDeviceTypes() {
    this.devicesService.getDeviceTypes().subscribe(types => {
      this.deviceTypes = types;
    })
  }

  updateDevice() {
    console.log('update');
    console.log(this.editDeviceForm.value);
    this.device = this.editDeviceForm.value;
    this.devicesService.updateDevice(this.device).subscribe(() => {
      this.toastr.success('Device updated successfully');
      this.editDeviceForm.reset(this.device);
    })
  }

}
