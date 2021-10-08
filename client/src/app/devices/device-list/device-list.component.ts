import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Device } from 'src/app/_models/device';
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

  constructor(private devicesService: DevicesService, public accountService: AccountService) { }

  ngOnInit(): void {
    this.loadDevices();
    this.loadDeviceTypes();
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
    if (event.target.checked) {
      this.devicesService.assignDevice(event.target.value).subscribe();
    } else {
      console.log('unchecked');
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
