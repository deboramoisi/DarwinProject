import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { $ } from 'protractor';
import { take } from 'rxjs/operators';
import { Device } from 'src/app/_models/device';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { DevicesService } from 'src/app/_services/devices.service';
import Swal from 'sweetalert2/dist/sweetalert2.js'; 

@Component({
  selector: 'app-device-list',
  templateUrl: './device-list.component.html',
  styleUrls: ['./device-list.component.css']
})
export class DeviceListComponent implements OnInit {
  devices: Device[];
  deviceTypes: string[];
  user: User;

  constructor(private devicesService: DevicesService, public accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadDevices();
    this.loadDeviceTypes();
    this.loadUser();
  }

  loadUser() {
    this.accountService.currentUser$.subscribe(user => {
      this.user = user;
    })
  }

  loadDevices() {
    this.devicesService.getDevices().subscribe(devices => {
      this.devices = devices;
    });
  }

  loadDeviceTypes() {
    this.devicesService.getDeviceTypes().subscribe(deviceTypes => {
      this.deviceTypes = deviceTypes;
    })
  }

  toggleAssignationDevice(event) {
    
    let modifiedDevice;
    const devId = event.target.value;

    modifiedDevice = this.devices.find(x => x.id == devId);

    if (event.target.checked) { 
      this.devicesService.assignDevice(devId).subscribe((device: Device) => {
        this.toastr.success("Device assigned successfully");
        modifiedDevice.userName = device.userName;
      })
    } else {
      this.devicesService.unassignDevice(devId).subscribe((device: Device) => {
        this.toastr.success("Device unassigned successfully"); 
        modifiedDevice.userName = device.userName;
      }) 
    }
  }

  deleteDevice(id: number) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      if (user.roles.includes("Admin")) {
        Swal.fire({
          title: 'Are you sure you want to delete the device?',
          text: 'You will not be able to recover it!',
          icon: 'warning',
          showCancelButton: true
        }).then((result) => {
          if (result.value) {
            this.devicesService.deleteDevice(id).subscribe(() => {
              this.devices = this.devices.filter(x => x.id != id);
            })        
          }
        })    
      } else {
        Swal("Access Unauthorized", "You are not allowed to delete devices!", "error");
      }
    })
  }

}
