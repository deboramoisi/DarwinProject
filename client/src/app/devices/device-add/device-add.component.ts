import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DevicesService } from 'src/app/_services/devices.service';

@Component({
  selector: 'app-device-add',
  templateUrl: './device-add.component.html',
  styleUrls: ['./device-add.component.css']
})
export class DeviceAddComponent implements OnInit {
  addDeviceForm: FormGroup;

  constructor(private fb: FormBuilder, private devicesService: DevicesService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.addDeviceForm = this.fb.group({
      name: ['', Validators.required],
      manufacturer: ['', Validators.required],
      type: ['', Validators.required],
      os: ['', Validators.required],
      osVersion: ['', Validators.required],
      processor: ['', Validators.required],
      ram: ['', Validators.required]
    })
  }

  addDevice() {
    this.devicesService.addDevice(this.addDeviceForm.value).subscribe(() => {
      this.toastr.success('Device added successfully');
    })
  }

}
